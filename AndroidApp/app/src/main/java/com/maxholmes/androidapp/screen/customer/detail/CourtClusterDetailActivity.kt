    package com.maxholmes.androidapp.screen.customer.detail

    import android.os.Bundle
    import androidx.activity.viewModels
    import androidx.appcompat.app.AppCompatActivity
    import androidx.recyclerview.widget.LinearLayoutManager
    import com.maxholmes.androidapp.R
    import com.maxholmes.androidapp.data.service.RetrofitClient
    import com.maxholmes.androidapp.data.model.Court
    import com.maxholmes.androidapp.data.model.CourtCluster
    import com.maxholmes.androidapp.databinding.ActivityCourtClusterDetailBinding
    import com.maxholmes.androidapp.screen.home.adapter.SelectCourtAdapter
    import com.maxholmes.androidapp.screen.home.adapter.SelectDayAdapter
    import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
    import com.maxholmes.androidapp.utils.ext.loadImageWithUrl
    import retrofit2.Call
    import retrofit2.Callback
    import retrofit2.Response
    import java.util.*

    class CourtClusterDetailActivity : AppCompatActivity() {

        private lateinit var binding: ActivityCourtClusterDetailBinding
        private val selectDayAdapter = SelectDayAdapter()
        private val selectCourtAdapter = SelectCourtAdapter()
        private var courtClusterId: String = ""

        override fun onCreate(savedInstanceState: Bundle?) {
            super.onCreate(savedInstanceState)
            binding = ActivityCourtClusterDetailBinding.inflate(layoutInflater)
            setContentView(binding.root)

            courtClusterId = intent.getStringExtra("courtClusterId") ?: throw IllegalArgumentException("courtClusterId is required")

            setupRecyclerViews()

            loadCourtClusterDetails()
            // Gọi API để lấy Courts theo CourtClusterId
            loadCourts()

            // Lấy danh sách ngày
            loadDays()
        }

        private fun setupRecyclerViews() {
            // RecyclerView cho ngày
            binding.recyclerViewDay.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
            binding.recyclerViewDay.adapter = selectDayAdapter

            binding.recyclerViewCourt.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
            binding.recyclerViewCourt.adapter = selectCourtAdapter

            selectDayAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Calendar> {
                override fun onItemClick(item: Calendar?) {
                    // Xử lý khi người dùng chọn ngày
                }
            })

            // Xử lý sự kiện chọn sân
            selectCourtAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Court> {
                override fun onItemClick(item: Court?) {
                    // Xử lý khi người dùng chọn sân
                }
            })
        }

        private fun loadCourtClusterDetails() {
            RetrofitClient.ApiClient.apiService.getCourtClusterById(courtClusterId).enqueue(object : Callback<CourtCluster> {
                override fun onResponse(call: Call<CourtCluster>, response: Response<CourtCluster>) {
                    if (response.isSuccessful) {
                        response.body()?.let { courtCluster ->
                            // Cập nhật thông tin chi tiết CourtCluster lên giao diện
                            binding.courtNameTextView.text = courtCluster.name
                            binding.courtDescriptionTextView.text = courtCluster.description ?: "Không có mô tả"
                            binding.addressCourtTextView.text = courtCluster.addressId // Có thể thêm thông tin địa chỉ chi tiết nếu cần
                            binding.workingTimeTextView.text = "${courtCluster.openingTime} - ${courtCluster.closingTime}"
                            // Cập nhật hình ảnh sân (nếu có)
                            // Nếu bạn muốn hiển thị ảnh, bạn có thể sử dụng thư viện như Glide hoặc Picasso để tải ảnh từ URL
                            courtCluster.imageUrl?.let {
                                binding.courtClusterImage.loadImageWithUrl(it) // Sử dụng hàm loadImageWithUrl
                            }
                        }
                    }
                }

                override fun onFailure(call: Call<CourtCluster>, t: Throwable) {
                    // Xử lý lỗi khi gọi API
                }
            })
        }

        private fun loadCourts() {
            RetrofitClient.ApiClient.apiService.getCourtsByClusterId(courtClusterId).enqueue(object : Callback<List<Court>> {
                override fun onResponse(call: Call<List<Court>>, response: Response<List<Court>>) {
                    if (response.isSuccessful) {
                        response.body()?.let {
                            // Cập nhật danh sách sân vào adapter
                            selectCourtAdapter.updateData(it.toMutableList())
                        }
                    }
                }

                override fun onFailure(call: Call<List<Court>>, t: Throwable) {
                    // Xử lý lỗi khi gọi API
                }
            })
        }

        private fun loadDays() {
            val calendar = Calendar.getInstance()
            val selectDays = mutableListOf<Calendar>()

            for (i in 0..6) {
                val day = calendar.clone() as Calendar
                day.add(Calendar.DAY_OF_YEAR, i)
                selectDays.add(day)
            }

            // Cập nhật danh sách ngày vào adapter
            selectDayAdapter.updateData(selectDays)
        }
    }
