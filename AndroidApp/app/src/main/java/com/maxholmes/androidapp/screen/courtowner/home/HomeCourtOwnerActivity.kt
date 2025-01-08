package com.maxholmes.androidapp.screen.courtowner.home

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
import com.maxholmes.androidapp.data.model.ImageCourtUrl
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityHomeCourtOwnerBinding
import com.maxholmes.androidapp.screen.courtowner.bookschedule.BookScheduleOwnerActivity
import com.maxholmes.androidapp.screen.courtowner.detail.CourtClusterEditActivity
import com.maxholmes.androidapp.screen.courtowner.register.RegisterCourtClusterActivity
import com.maxholmes.androidapp.screen.courtowner.statistics.StatisticActivity
import com.maxholmes.androidapp.screen.customer.bookschedule.BookScheduleActivity
import com.maxholmes.androidapp.screen.customer.home.adapter.CourtClusterCourtOwnerAdapter
import com.maxholmes.androidapp.screen.customer.search.SearchActivity
import com.maxholmes.androidapp.screen.user.UserActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.authentication.getRoleFromToken
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class HomeCourtOwnerActivity : AppCompatActivity() {

    private lateinit var binding: ActivityHomeCourtOwnerBinding
    private lateinit var courtClusterAdapter: CourtClusterCourtOwnerAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityHomeCourtOwnerBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val token = SharedPreferencesUtils.getToken(this)
        courtClusterAdapter = CourtClusterCourtOwnerAdapter()

        binding.recyclerViewCourtCluster.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewCourtCluster.adapter = courtClusterAdapter

        fetchCourtClusters()

        binding.bottomNavigationView.selectedItemId = R.id.home
        setupBottomNavigation(token!!)

        courtClusterAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtCluster> {
            override fun onItemClick(item: CourtCluster?) {
                item?.let {
                    val intent = Intent(this@HomeCourtOwnerActivity, CourtClusterEditActivity::class.java)
                    intent.putExtra("courtCluster", it)
                    startActivity(intent)
                }
            }
        })

        binding.backButton.setOnClickListener { finish() }

        binding.btnStats.setOnClickListener {
            val intent = Intent(this@HomeCourtOwnerActivity, StatisticActivity::class.java)
            startActivity(intent)
        }

        binding.btnAddNew.setOnClickListener {
            val intent = Intent(this@HomeCourtOwnerActivity, RegisterCourtClusterActivity::class.java)
            startActivity(intent)
        }
    }

    private fun fetchCourtClusters() {
        val token = SharedPreferencesUtils.getToken(this)
        if (token.isNullOrEmpty()) {
            Toast.makeText(this@HomeCourtOwnerActivity, "Token không hợp lệ!", Toast.LENGTH_SHORT).show()
            return
        }
        val tokenHeader = "Bearer $token"
        RetrofitClient.ApiClient.apiService.getCourtClustersByOwner(token = tokenHeader).enqueue(object :
            Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val courtClusterResponses: List<CourtClusterResponse>? = parseApiResponseData(apiResponse.data)
                        val courtClusters: MutableList<CourtCluster> = mutableListOf()
                        courtClusterResponses?.forEach { courtClusterResponse ->
                            RetrofitClient.ApiClient.apiService.getAddressById(courtClusterResponse.addressId).enqueue(object :
                                Callback<APIResponse> {
                                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                                    if (response.isSuccessful) {
                                        response.body()?.let { apiResponse ->
                                            val address: Address? = parseApiResponseData(apiResponse.data)
                                            if (address == null) {
                                                println("Địa chỉ lỗi, kiểm tra lại backend")
                                            } else {
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

                                                courtClusters.add(courtCluster)
                                                courtClusterAdapter.updateData(courtClusters.toMutableList())
                                            }
                                        }
                                    }
                                }

                                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                                    Toast.makeText(this@HomeCourtOwnerActivity, "Không lấy được dữ liệu địa chỉ: ${t.message}", Toast.LENGTH_SHORT).show()
                                }
                            })
                        }
                        courtClusterAdapter.updateData(courtClusters.toMutableList())
                    }
                } else {
                    Toast.makeText(this@HomeCourtOwnerActivity, "Lấy dữ liệu sân bị lỗi, vui lòng thử lại sau.", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@HomeCourtOwnerActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun setupBottomNavigation(token: String) {
        binding.bottomNavigationView.setOnItemSelectedListener { item ->
            when (item.itemId) {
                R.id.home -> true
                R.id.booking -> {
                    val intent = Intent().apply {
                        setClass(this@HomeCourtOwnerActivity, BookScheduleOwnerActivity::class.java)
                    }
                    startActivity(intent)
                    true
                }
                R.id.search -> {
                    val intent = Intent(this@HomeCourtOwnerActivity, SearchActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.user -> {
                    val intent = Intent(this@HomeCourtOwnerActivity, UserActivity::class.java)
                    startActivity(intent)
                    true
                }
                else -> false
            }
        }
    }
}
