package com.maxholmes.androidapp.screen.courtowner.statistics

import android.os.Bundle
import android.util.Log
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.StatisticDto
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityStatisticBinding
import com.maxholmes.androidapp.screen.courtowner.statistics.adapter.StatisticAdapter
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.time.LocalDate
import java.time.format.DateTimeFormatter

class StatisticActivity : AppCompatActivity() {

    private lateinit var binding: ActivityStatisticBinding
    private lateinit var statisticAdapter: StatisticAdapter
    private val statistics = mutableListOf<StatisticDto>()

    private var filterDays = 30 // Mặc định là 30 ngày
    private var sortBy = "totalRevenue" // Mặc định sắp xếp theo doanh thu
    private var isAscending = true // Mặc định là tăng dần

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityStatisticBinding.inflate(layoutInflater)
        setContentView(binding.root)

        statisticAdapter = StatisticAdapter()
        binding.recyclerView.layoutManager = LinearLayoutManager(this)
        binding.recyclerView.adapter = statisticAdapter
        binding.backButton.setOnClickListener { finish() }

        setupSpinners()
        fetchStatistics()
    }

    private fun setupSpinners() {
        val filterOptions1 = resources.getStringArray(R.array.filter_options1)
        val filterOptions2 = resources.getStringArray(R.array.filter_options2)
        val filterOptions3 = resources.getStringArray(R.array.filter_options3)

        val adapter1 = ArrayAdapter(this, android.R.layout.simple_spinner_item, filterOptions1)
        val adapter2 = ArrayAdapter(this, android.R.layout.simple_spinner_item, filterOptions2)
        val adapter3 = ArrayAdapter(this, android.R.layout.simple_spinner_item, filterOptions3)

        adapter1.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        adapter2.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        adapter3.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)

        binding.spinnerFilter1.adapter = adapter1
        binding.spinnerFilter2.adapter = adapter2
        binding.spinnerFilter3.adapter = adapter3

        binding.spinnerFilter1.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(parent: AdapterView<*>?, view: android.view.View?, position: Int, id: Long) {
                sortBy = if (position == 0) "totalRevenue" else "totalBookings"
                fetchStatistics()
            }

            override fun onNothingSelected(parent: AdapterView<*>?) {}
        }

        binding.spinnerFilter2.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(parent: AdapterView<*>?, view: android.view.View?, position: Int, id: Long) {
                filterDays = when (position) {
                    0 -> 7
                    1 -> 30
                    else -> 90
                }
                fetchStatistics()
            }

            override fun onNothingSelected(parent: AdapterView<*>?) {}
        }

        binding.spinnerFilter3.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(parent: AdapterView<*>?, view: android.view.View?, position: Int, id: Long) {
                isAscending = position == 0
                fetchStatistics()
            }

            override fun onNothingSelected(parent: AdapterView<*>?) {}
        }
    }

    private fun fetchStatistics() {
        val token = SharedPreferencesUtils.getToken(this)
        val headerAuthorize = "Bearer $token"

        val endDate = LocalDate.now()
        val startDate = endDate.minusDays(filterDays.toLong())
        val dateFormatter = DateTimeFormatter.ofPattern("yyyy-MM-dd")

        RetrofitClient.ApiClient.apiService.getStatistic(
            token = headerAuthorize,
            startDate = startDate.format(dateFormatter),
            endDate = endDate.format(dateFormatter)
        ).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val parsedStatistics: List<StatisticDto>? = parseApiResponseData(apiResponse.data)
                        if (!parsedStatistics.isNullOrEmpty()) {
                            statistics.clear()
                            statistics.addAll(parsedStatistics)

                            // Sắp xếp theo tiêu chí
                            statistics.sortWith(compareBy { if (sortBy == "totalRevenue") it.totalRevenue else it.totalBookings })
                            if (!isAscending) statistics.reverse()

                            statisticAdapter.updateData(statistics)
                            updateSummary(statistics)
                        } else {
                            Toast.makeText(this@StatisticActivity, "Không có dữ liệu thống kê", Toast.LENGTH_SHORT).show()
                        }
                    }
                } else {
                    Toast.makeText(this@StatisticActivity, "Lỗi khi lấy dữ liệu", Toast.LENGTH_SHORT).show()
                    Log.e("StatisticActivity", "Error: ${response.message()}")
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@StatisticActivity, "Không thể kết nối đến server", Toast.LENGTH_SHORT).show()
                Log.e("StatisticActivity", "Failure: ${t.message}")
            }
        })
    }

    private fun updateSummary(statistics: List<StatisticDto>) {
        val totalRevenue = statistics.sumOf { it.totalRevenue }
        val totalBookings = statistics.sumOf { it.totalBookings }

        binding.tvTotalRevenue.text = "Tổng doanh thu: ${totalRevenue}₫"
        binding.tvTotalOrders.text = "Tổng số lượt đặt: $totalBookings"
    }
}
