package com.maxholmes.androidapp.screen.customer.home

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.CourtClusterResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.screen.customer.home.adapter.CourtClusterAdapter
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityHomeCustomerBinding
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.customer.bookschedule.BookScheduleActivity
import com.maxholmes.androidapp.screen.customer.detail.CourtClusterDetailActivity
import com.maxholmes.androidapp.screen.customer.search.SearchActivity
import com.maxholmes.androidapp.screen.user.UserActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.authentication.getRoleFromToken
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class HomeCustomerActivity : AppCompatActivity() {

    private lateinit var binding: ActivityHomeCustomerBinding
    private lateinit var courtClusterAdapter: CourtClusterAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityHomeCustomerBinding.inflate(layoutInflater)
        setContentView(binding.root)
        val token = SharedPreferencesUtils.getToken(this)
        courtClusterAdapter = CourtClusterAdapter()
        binding.recyclerViewCourtCluster.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewCourtCluster.adapter = courtClusterAdapter
        binding.bottomNavigationView.selectedItemId = R.id.home
        setupBottomNavigation(token!!)
        fetchCourtClusters()

        courtClusterAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtCluster> {
            override fun onItemClick(item: CourtCluster?) {
                item?.let {
                    val intent = Intent(this@HomeCustomerActivity, CourtClusterDetailActivity::class.java)
                    intent.putExtra("courtCluster", it)
                    startActivity(intent)
                }
            }
        })
    }

    private fun fetchCourtClusters() {
        RetrofitClient.ApiClient.apiService.getActiveCourtClusters().enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val courtClusterResponses: List<CourtClusterResponse>? = parseApiResponseData(apiResponse.data)
                        var courtClusters: MutableList<CourtCluster> = mutableListOf()
                        courtClusterResponses?.forEach { courtClusterResponse ->
                            RetrofitClient.ApiClient.apiService.getAddressById(courtClusterResponse.addressId).enqueue(object : Callback<APIResponse> {
                                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                                    if (response.isSuccessful) {
                                        response.body()?.let { apiResponse ->
                                            val address: Address? = parseApiResponseData(apiResponse.data)
                                            if(address == null) {
                                                println("Địa chỉ lỗi, kiểm tra lại backend")
                                            }
                                            val courtCluster = CourtCluster(
                                                id = courtClusterResponse.id,
                                                name = courtClusterResponse.name,
                                                openingTime = courtClusterResponse.openingTime,
                                                closingTime = courtClusterResponse.closingTime,
                                                description = courtClusterResponse.description,
                                                address = address!!,
                                                courtOwnerId = courtClusterResponse.courtOwnerId,
                                            )
                                            courtClusters.add(courtCluster)
                                            courtClusterAdapter.updateData(courtClusters.toMutableList())
                                        }
                                    }
                                }

                                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                                    Toast.makeText(this@HomeCustomerActivity, "Không lấy được dữ liệu địa chỉ: ${t.message}", Toast.LENGTH_SHORT).show()
                                }
                            })
                        }
                        courtClusterAdapter.updateData(courtClusters.toMutableList())
                    }
                } else {
                    Toast.makeText(this@HomeCustomerActivity, "Lấy dữ liệu sân bị lỗi, vui lòng thử lại sau.", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@HomeCustomerActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun setupBottomNavigation(token: String) {
        binding.bottomNavigationView.setOnItemSelectedListener { item ->
            when (item.itemId) {
                R.id.home -> {
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
