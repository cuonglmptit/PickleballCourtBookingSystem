package com.maxholmes.androidapp.screen.customer.detail

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.CourtOwnerInfoDto
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.model.ImageCourtUrl
import com.maxholmes.androidapp.databinding.ActivityCourtClusterDetailBinding
import com.maxholmes.androidapp.screen.customer.booking.BookingActivity
import com.maxholmes.androidapp.screen.home.adapter.SelectCourtAdapter
import com.maxholmes.androidapp.screen.home.adapter.SelectDayAdapter
import com.maxholmes.androidapp.screen.test.TestActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.formatDate
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl
import com.maxholmes.androidapp.utils.ext.loadImageWithUrl
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.util.*

class CourtClusterDetailActivity : AppCompatActivity() {

    private lateinit var binding: ActivityCourtClusterDetailBinding
    private val selectDayAdapter = SelectDayAdapter()
    private val selectCourtAdapter = SelectCourtAdapter()
    private var selectedDay: Calendar? = null
    private var selectedCourt: Court? = null
    private var courtCluster: CourtCluster? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityCourtClusterDetailBinding.inflate(layoutInflater)
        setContentView(binding.root)

        courtCluster = intent.getParcelableExtra("courtCluster", CourtCluster::class.java)
        if(courtCluster == null)
        {
            Toast.makeText(this, "San bị lỗi vui lòng thử lại sau.", Toast.LENGTH_SHORT).show()
            finish()
        }

        setupRecyclerViews()

        loadCourtClusterDetails()
        loadCourtOwnerInfo()
        loadImage()
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

            val intent = Intent(this, BookingActivity::class.java)
            intent.putExtra("selectedDay", formattedDate)
            intent.putExtra("court", selectedCourt!!.id)

            intent.putParcelableArrayListExtra("courts", ArrayList(selectCourtAdapter.getCourts()))

            startActivity(intent)
        }
    }


    private fun loadCourtClusterDetails() {
        binding.courtNameTextView.text = courtCluster!!.name
        binding.courtDescriptionTextView.text = courtCluster!!.description
        binding.addressCourtTextView.text = courtCluster!!.address.toString()
        binding.starValueTextView.text = "0"
        val timeWorking = courtCluster!!.openingTime + " - " + courtCluster!!.closingTime
        binding.workingTimeTextView.text = timeWorking
    }

    private fun loadImage() {
        RetrofitClient.ApiClient.apiService.getImagesByCourtClusterId(courtCluster!!.id)
            .enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val imageList: List<ImageCourtUrl>? = parseApiResponseData(apiResponse.data)
                            imageList.let {
                                if(it.isNullOrEmpty())
                                {
                                    binding.courtClusterImage.setImageResource(R.drawable.image_court_1)
                                }
                                else
                                {
                                    binding.courtClusterImage.loadImageCircleWithUrl(
                                        it[0].url,
                                        R.drawable.image_court_1
                                    )
                                }
                            }
                        }
                    } else {
                        Toast.makeText(
                            this@CourtClusterDetailActivity,
                            "Lỗi khi tải hình ảnh!",
                            Toast.LENGTH_SHORT
                        ).show()
                        binding.courtClusterImage.setImageResource(R.drawable.image_court_1)
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    Toast.makeText(
                        this@CourtClusterDetailActivity,
                        "Lỗi kết nối mạng. Vui lòng thử lại!",
                        Toast.LENGTH_SHORT
                    ).show()
                    // Load ảnh mặc định nếu kết nối thất bại
                    binding.courtClusterImage.setImageResource(R.drawable.image_court_1)
                }
            })
    }


    private fun loadCourts() {
        RetrofitClient.ApiClient.apiService.getCourtsByCourtClusterId(courtCluster!!.id)
            .enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val courts: List<Court>? = parseApiResponseData(apiResponse.data)
                            courts?.let {
                                val sortedCourts = it.sortedBy { court -> court.courtNumber }
                                selectCourtAdapter.updateData(sortedCourts.toMutableList())
                            }
                        }
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    Toast.makeText(this@CourtClusterDetailActivity, "Lỗi khi tải danh sách sân!", Toast.LENGTH_SHORT).show()
                }
            })
    }


    private fun loadCourtOwnerInfo() {
        val token = SharedPreferencesUtils.getToken(this)
        if (token.isNullOrEmpty()) {
            Toast.makeText(this@CourtClusterDetailActivity, "Token không hợp lệ!", Toast.LENGTH_SHORT).show()
            return
        }
        val tokenHeader = "Bearer $token"

        RetrofitClient.ApiClient.apiService.getCourtOwnerInfo(courtCluster!!.courtOwnerId, tokenHeader).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val courtOwnerInfo : CourtOwnerInfoDto? = parseApiResponseData(apiResponse.data)
                        courtOwnerInfo?.let {
                            binding.phoneNumberTextView.text = it.phoneNumber
                        }
                    }
                } else {
                    Toast.makeText(this@CourtClusterDetailActivity, "Lỗi khi tải thông tin chủ sân!", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@CourtClusterDetailActivity, "Lỗi kết nối mạng. Vui lòng thử lại!", Toast.LENGTH_SHORT).show()
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
