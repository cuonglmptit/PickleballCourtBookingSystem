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

class SearchActivity : AppCompatActivity() {

    private lateinit var binding: ActivitySearchBinding
    private lateinit var courtClusterAdapter: CourtClusterAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivitySearchBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val token = SharedPreferencesUtils.getToken(this)
        courtClusterAdapter = CourtClusterAdapter()
        binding.recyclerViewSearchResults.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewSearchResults.adapter = courtClusterAdapter

        binding.buttonSearch.setOnClickListener {
            val name = binding.editTextCourtName.text.toString()
            val city = binding.editTextCityName.text.toString()
            val date = binding.editTextDate.text.toString()
            val startTime = binding.editTextStartTime.text.toString()
            val endTime = binding.editTextEndTime.text.toString()

            searchCourtClusters(name, city, date, startTime, endTime)
        }

        courtClusterAdapter.registerItemRecyclerViewClickListener(object :
            OnItemRecyclerViewClickListener<CourtCluster> {
            override fun onItemClick(item: CourtCluster?) {
                item?.let {
                    val intent = Intent(this@SearchActivity, CourtClusterDetailActivity::class.java)
                    intent.putExtra("courtCluster", it)
                    startActivity(intent)
                    Toast.makeText(this@SearchActivity, "Clicked on: ${it.name}", Toast.LENGTH_SHORT).show()
                }
            }
        })

        binding.bottomNavigationView.selectedItemId = R.id.search
        setupBottomNavigation(token!!)
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
                                                )
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
                        Toast.makeText(this@SearchActivity, "Lỗi tìm kiếm, vui lòng thử lại.", Toast.LENGTH_SHORT).show()
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
