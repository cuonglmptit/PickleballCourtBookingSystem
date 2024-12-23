package com.maxholmes.androidapp.screen.customer.home

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.screen.customer.home.adapter.CourtClusterAdapter
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.screen.customer.detail.CourtClusterDetailActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class HomeCustomerActivity : AppCompatActivity() {

    private lateinit var courtClusterAdapter: CourtClusterAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home_customer)

        // Set up RecyclerView and Adapter
        courtClusterAdapter = CourtClusterAdapter()
        val recyclerView = findViewById<androidx.recyclerview.widget.RecyclerView>(R.id.recyclerViewCourtCluster)
        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.adapter = courtClusterAdapter

        // Fetch the CourtCluster data from the API
        fetchCourtClusters()

        // Handle item click events
        courtClusterAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtCluster> {
            override fun onItemClick(item: CourtCluster?) {
                item?.let {
                    // Handle item click, e.g., navigate to a detailed screen or show details
                    val intent = Intent(this@HomeCustomerActivity, CourtClusterDetailActivity::class.java)
                    intent.putExtra("courtClusterId", it.id)
                    startActivity(intent)
                    Toast.makeText(this@HomeCustomerActivity, "Clicked on: ${it.name}", Toast.LENGTH_SHORT).show()
                }
            }
        })
    }

    private fun fetchCourtClusters() {
        // Fetch data from the API using Retrofit
        RetrofitClient.ApiClient.apiService.getAllCourtClusters().enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val courtClusters: List<CourtCluster>? =
                            parseApiResponseData(apiResponse.data)
                        courtClusters.let {
                            courtClusterAdapter.updateData(it!!.toMutableList())
                        }
                    }
                } else {
                    // Handle unsuccessful response
                    Toast.makeText(this@HomeCustomerActivity, "Failed to load data", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                // Handle failure, e.g., no internet connection
                Toast.makeText(this@HomeCustomerActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }
}
