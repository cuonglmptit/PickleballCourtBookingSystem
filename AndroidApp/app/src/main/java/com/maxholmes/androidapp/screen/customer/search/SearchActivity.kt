package com.maxholmes.androidapp.screen.customer.search

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.CourtClusterResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivitySearchBinding
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.customer.bookschedule.BookScheduleActivity
import com.maxholmes.androidapp.screen.customer.detail.CourtClusterDetailActivity
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.screen.customer.home.adapter.CourtClusterAdapter
import com.maxholmes.androidapp.screen.user.UserActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.authentication.getRoleFromToken
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

import android.app.DatePickerDialog
import android.widget.ArrayAdapter
import android.widget.DatePicker
import com.google.gson.JsonObject
import com.maxholmes.androidapp.data.model.ImageCourtUrl
import com.maxholmes.androidapp.utils.ext.formatDateToRequest
import com.maxholmes.androidapp.utils.ext.formatDateToShow
import com.maxholmes.androidapp.utils.ext.generateTimeList
import org.json.JSONObject
import java.text.SimpleDateFormat
import java.util.*

class SearchActivity : AppCompatActivity() {

    private lateinit var binding: ActivitySearchBinding
    private lateinit var courtClusterAdapter: CourtClusterAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivitySearchBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val token = SharedPreferencesUtils.getToken(this)
        courtClusterAdapter = CourtClusterAdapter()
        val timeOptions = generateTimeList()
        val timeAdapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, timeOptions)
        timeAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.recyclerViewSearchResults.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewSearchResults.adapter = courtClusterAdapter
        val todayDate = Calendar.getInstance().formatDateToShow("dd/MM/yyyy")
        binding.textViewDate.text = todayDate
        binding.spinnerStartTime.adapter = timeAdapter
        binding.spinnerEndTime.adapter = timeAdapter
        binding.spinnerStartTime.setSelection(timeOptions.indexOf("05:00"))
        binding.spinnerEndTime.setSelection(timeOptions.indexOf("22:00"))



        binding.buttonSearch.setOnClickListener {
            val name = binding.editTextCourtName.text.toString()
            val city = binding.editTextCityName.text.toString()
            val date = binding.textViewDate.text.toString().formatDateToRequest()
            val startTime = binding.spinnerStartTime.selectedItem.toString()
            val endTime = binding.spinnerEndTime.selectedItem.toString()

            searchCourtClusters(name, city, date, startTime, endTime)
        }

        binding.selectDayButton.setOnClickListener {
            showDatePickerDialog()
        }

        courtClusterAdapter.registerItemRecyclerViewClickListener(object :
            OnItemRecyclerViewClickListener<CourtCluster> {
            override fun onItemClick(item: CourtCluster?) {
                item?.let {
                    val intent = Intent(this@SearchActivity, CourtClusterDetailActivity::class.java)
                    intent.putExtra("courtCluster", it)
                    startActivity(intent)
                }
            }
        })

        binding.bottomNavigationView.selectedItemId = R.id.search
        setupBottomNavigation(token!!)
    }

    private fun showDatePickerDialog() {
        val calendar = Calendar.getInstance()

        val year = calendar.get(Calendar.YEAR)
        val month = calendar.get(Calendar.MONTH)
        val day = calendar.get(Calendar.DAY_OF_MONTH)

        val datePickerDialog = DatePickerDialog(this, { view: DatePicker, year: Int, monthOfYear: Int, dayOfMonth: Int ->
            val selectedDateFormatted = String.format("%02d/%02d/%04d", dayOfMonth, monthOfYear + 1, year)
            binding.textViewDate.setText(selectedDateFormatted)
        }, year, month, day)

        datePickerDialog.datePicker.minDate = calendar.timeInMillis
        datePickerDialog.show()
    }


    private fun searchCourtClusters(
        name: String,
        city: String,
        date: String,
        startTime: String,
        endTime: String
    ) {
        RetrofitClient.ApiClient.apiService.searchCourtClusters(name, city, date, startTime, endTime)
            .enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        val courtClusterResponses: List<CourtClusterResponse>? = parseApiResponseData(response.body()?.data)
                        val courtClusters = mutableListOf<CourtCluster>()

                        courtClusterResponses?.forEach { courtClusterResponse ->
                            RetrofitClient.ApiClient.apiService.getAddressById(courtClusterResponse.addressId)
                                .enqueue(object : Callback<APIResponse> {
                                    override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                                        if (response.isSuccessful) {
                                            val address = parseApiResponseData<Address>(response.body()?.data)
                                            if (address != null) {
                                                val courtCluster = CourtCluster(
                                                    id = courtClusterResponse.id,
                                                    name = courtClusterResponse.name,
                                                    openingTime = courtClusterResponse.openingTime,
                                                    closingTime = courtClusterResponse.closingTime,
                                                    description = courtClusterResponse.description,
                                                    address = address,
                                                    courtOwnerId = courtClusterResponse.courtOwnerId,
                                                    status = courtClusterResponse.status
                                                )
                                                RetrofitClient.ApiClient.apiService.getImagesByClusterId(courtCluster.id).enqueue(object: Callback<APIResponse> {
                                                    override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                                                        if (response.isSuccessful) {
                                                            response.body()?.let { apiResponse ->
                                                                val images: List<ImageCourtUrl>? = parseApiResponseData(apiResponse.data)
                                                                courtCluster.imageUrl = images?.get(0)?.url
                                                            }
                                                        }
                                                    }
                                                    override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                                                        Toast.makeText(this@SearchActivity, "Không lấy được dữ liệu hình ảnh: ${t.message}", Toast.LENGTH_SHORT).show()
                                                    }

                                                })
                                                courtClusters.add(courtCluster)

                                                courtClusterAdapter.updateData(courtClusters.toMutableList())
                                            }
                                        } else {
                                            Toast.makeText(
                                                this@SearchActivity,
                                                "Không tìm thấy địa chỉ cho sân: ${courtClusterResponse.name}",
                                                Toast.LENGTH_SHORT
                                            ).show()
                                        }
                                    }

                                    override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                                        Toast.makeText(this@SearchActivity, "Lỗi lấy địa chỉ: ${t.message}", Toast.LENGTH_SHORT).show()
                                    }
                                })
                        }

                        if (courtClusterResponses.isNullOrEmpty()) {
                            Toast.makeText(this@SearchActivity, "Không tìm thấy sân nào.", Toast.LENGTH_SHORT).show()
                        }
                    } else {
                        val data = JSONObject(response.errorBody()?.string())
                        val text: String = parseApiResponseData(data.get("userMsg"))!!
                        Toast.makeText(this@SearchActivity, text, Toast.LENGTH_SHORT).show()
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    Toast.makeText(this@SearchActivity, "Lỗi: ${t.message}", Toast.LENGTH_SHORT).show()
                }
            })
    }

    private fun setupBottomNavigation(token: String) {
        binding.bottomNavigationView.setOnItemSelectedListener { item ->
            when (item.itemId) {
                R.id.home -> {
                    val intent = Intent()
                    if (getRoleFromToken(token) == "Customer") {
                        intent.setClass(this, HomeCustomerActivity::class.java)
                    } else if (getRoleFromToken(token) == "CourtOwner") {
                        intent.setClass(this, HomeCourtOwnerActivity::class.java)
                    }

                    startActivity(intent)
                    true
                }

                R.id.booking -> {
                    val intent = Intent()
                    if(getRoleFromToken(token) == "Customer") {
                        intent.setClass(this, BookScheduleActivity::class.java)
                    } else if(getRoleFromToken(token) == "CourtOwner") {
                        intent.setClass(this, BookScheduleActivity::class.java)
                    }
                    startActivity(intent)
                    true
                }

                R.id.search -> {
                    val intent = Intent(this, SearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.user -> {
                    val intent = Intent(this, UserActivity::class.java)
                    startActivity(intent)
                    true
                }

                else -> false
            }
        }
    }
}

