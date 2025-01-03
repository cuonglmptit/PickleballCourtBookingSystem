package com.maxholmes.androidapp.screen.courtowner.home

import android.content.Intent
import android.os.Bundle
import android.widget.ImageButton
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.CourtClusterResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.screen.courtowner.detail.CourtClusterEditActivity
import com.maxholmes.androidapp.screen.courtowner.register.RegisterCourtClusterActivity
import com.maxholmes.androidapp.screen.courtowner.statistics.StatisticActivity
import com.maxholmes.androidapp.screen.customer.home.adapter.CourtClusterCourtOwnerAdapter
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class HomeCourtOwnerActivity : AppCompatActivity() {

    private lateinit var courtClusterAdapter: CourtClusterCourtOwnerAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home_court_owner)

        courtClusterAdapter = CourtClusterCourtOwnerAdapter()
        val recyclerView = findViewById<RecyclerView>(R.id.recyclerViewCourtCluster)
        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.adapter = courtClusterAdapter

        fetchCourtClusters()

        // Handle item click events
        courtClusterAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtCluster> {
            override fun onItemClick(item: CourtCluster?) {
                item?.let {
                    // Navigate to edit screen
                    val intent = Intent(this@HomeCourtOwnerActivity, CourtClusterEditActivity::class.java)
                    intent.putExtra("courtClusterId", it.id)
                    startActivity(intent)
                }
            }
        })

        // Set up back button
        findViewById<ImageButton>(R.id.backButton).setOnClickListener { finish() }

        // Set up statistics button
        findViewById<android.widget.Button>(R.id.btn_stats).setOnClickListener {
            val intent = Intent(this@HomeCourtOwnerActivity, StatisticActivity::class.java)
            startActivity(intent)
        }

        // Set up add new button
        findViewById<android.widget.Button>(R.id.btn_add_new).setOnClickListener {
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
                        var courtClusters: MutableList<CourtCluster> = mutableListOf()
                        courtClusterResponses?.forEach { courtClusterResponse ->
                            RetrofitClient.ApiClient.apiService.getAddressById(courtClusterResponse.addressId).enqueue(object :
                                Callback<APIResponse> {
                                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                                    if (response.isSuccessful) {
                                        response.body()?.let { apiResponse ->
                                            val address: Address? = parseApiResponseData(apiResponse.data)
                                            if(address == null)
                                            {
                                                println("Dia chi loi kiem tra lai backend")
                                            }
                                            var courtCluster = CourtCluster(
                                                id = courtClusterResponse.id,
                                                name = courtClusterResponse.name,
                                                openingTime = courtClusterResponse.openingTime,
                                                closingTime = courtClusterResponse.closingTime,
                                                description = courtClusterResponse.description,
                                                address = address!!,
                                                courtOwnerId = courtClusterResponse.courtOwnerId,
                                                imageUrl = courtClusterResponse.imageUrl
                                            )
                                            courtClusters.add(courtCluster)
                                            courtClusterAdapter.updateData(courtClusters.toMutableList())
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
                    Toast.makeText(this@HomeCourtOwnerActivity, "Lấy dữ liệu sân bị lỗi vui lòng thử lại sau.", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@HomeCourtOwnerActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }
}