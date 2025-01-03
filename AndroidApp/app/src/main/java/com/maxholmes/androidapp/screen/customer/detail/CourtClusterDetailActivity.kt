package com.maxholmes.androidapp.screen.customer.detail

import android.content.Intent
import android.os.Bundle
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.databinding.ActivityCourtClusterDetailBinding
import com.maxholmes.androidapp.screen.home.adapter.SelectCourtAdapter
import com.maxholmes.androidapp.screen.home.adapter.SelectDayAdapter
import com.maxholmes.androidapp.screen.test.TestActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.formatDate
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
    private var selectedDay: Calendar? = null
    private var selectedCourt: Court? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityCourtClusterDetailBinding.inflate(layoutInflater)
        setContentView(binding.root)

//        courtClusterId = intent.getStringExtra("courtClusterId") ?: throw IllegalArgumentException("courtClusterId is required")

        setupRecyclerViews()

        loadCourtClusterDetails()
        loadCourts()
        loadDays()
    }

    private fun setupRecyclerViews() {
        binding.recyclerViewDay.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewDay.adapter = selectDayAdapter

        binding.recyclerViewCourt.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewCourt.adapter = selectCourtAdapter

        selectDayAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Calendar> {
            override fun onItemClick(item: Calendar?) {
                item?.let {
                    val position = selectDayAdapter.getSelectDays().indexOf(it)
                    selectDayAdapter.setSelectedItem(position)
                    selectedDay = it
                    checkAndNavigate()
                }
            }
        })

        selectCourtAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Court> {
            override fun onItemClick(item: Court?) {
                item?.let {
                    val position = selectCourtAdapter.getCourts().indexOf(it)
                    selectCourtAdapter.setSelectedItem(position)
                    selectedCourt = it
                    checkAndNavigate()
                }
            }
        })
    }

    private fun checkAndNavigate() {
        if (selectedDay != null && selectedCourt != null) {
            val formattedDate = selectedDay!!.formatDate()

            val intent = Intent(this, TestActivity::class.java)
            intent.putExtra("selectedDay", formattedDate)
            intent.putExtra("courtId", selectedCourt!!.id)
            startActivity(intent)
        }
    }

    private fun loadCourtClusterDetails() {
        binding.courtNameTextView.text = "Sân Sao Mai"
        binding.courtDescriptionTextView.text = "Không có mô tả"
        binding.addressCourtTextView.text = "Đường Xuân Thủy, Phường Dịch Vọng, Quận Cầu Giấy, Hà Nội"
        binding.phoneNumberTextView.text = "0123456789"
        binding.starValueTextView.text = "0"
        binding.workingTimeTextView.text = "05:00 - 22:00"
        binding.courtClusterImage.setImageResource(R.drawable.image_court_1)
//        RetrofitClient.ApiClient.apiService.getCourtClusterById(courtClusterId).enqueue(object : Callback<APIResponse> {
//            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
//                if (response.isSuccessful) {
//                    response.body()?.let { apiResponse ->
//                        val courtCluster: CourtCluster? = parseApiResponseData(apiResponse.data)
//                        courtCluster?.let {
////                            binding.courtNameTextView.text = courtCluster.name
//
////                            binding.courtDescriptionTextView.text = courtCluster.description ?: "Không có mô tả"
//
//
////                            loadAddressById(courtCluster.addressId)
////                            binding.addressCourtTextView.text = "Đường Xuân Thủy, Phường Dịch Vọng, Quận Cầu Giấy, Hà Nội"
////                            binding.phoneNumberTextView.text = "0123456789"
////                            binding.starValueTextView.text = "0"
////                            binding.workingTimeTextView.text = "${courtCluster.openingTime} - ${courtCluster.closingTime}"
//
////                            courtCluster.imageUrl?.let {
////                                binding.courtClusterImage.loadImageWithUrl(it)
////                            }
////                            binding.workingTimeTextView.text = "05:00 - 22:00"
////                            binding.courtClusterImage.setImageResource(R.drawable.image_court_1)
////                            courtCluster.imageUrl?.let {
////                                binding.courtClusterImage.loadImageWithUrl(it)
////                            }
//                        }
//                    }
//                }
//            }
//
//            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
//                // Xử lý khi gọi API không thành công (nếu cần)
//            }
//        })
    }


    private fun loadCourts() {
        var courts = mutableListOf<Court>(
            Court(
                id = "test",
                courtNumber = 1,
                description = null,
                courtClusterId = "test"
            ),
            Court(
                id = "test",
                courtNumber = 2,
                description = null,
                courtClusterId = "test"
            )
        )
        selectCourtAdapter.updateData(courts)
//        RetrofitClient.ApiClient.apiService.getCourtsByCourtClusterId(courtClusterId).enqueue(object : Callback<APIResponse> {
//            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
//                if (response.isSuccessful) {
//                    response.body()?.let { apiReponse ->
//                        val courts: List<Court>? = parseApiResponseData(apiReponse.data)
//
//                        courts.let {
//                            selectCourtAdapter.updateData(it!!.toMutableList())
//                        }
//
//                    }
//                }
//            }
//
//            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
//            }
//        })
    }

    private fun loadAddressById(addressId: String) {
        RetrofitClient.ApiClient.apiService.getAddressById(addressId).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val address: Address? = parseApiResponseData(apiResponse.data)
                        address?.let {
                            // Hiển thị địa chỉ đầy đủ
                            val fullAddress = "${it.street}, ${it.ward}, ${it.district}, ${it.city}"
                            binding.addressCourtTextView.text = fullAddress
                        }
                    }
                } else {
                    binding.addressCourtTextView.text = "Không có thông tin địa chỉ"
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                binding.addressCourtTextView.text = "Lỗi khi lấy thông tin địa chỉ"
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

        selectDayAdapter.updateData(selectDays)
    }
}
