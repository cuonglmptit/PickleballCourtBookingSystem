package com.maxholmes.androidapp.screen.courtowner.home

import android.content.Intent
import android.os.Bundle
import android.widget.ImageButton
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.screen.courtowner.detail.CourtClusterEditActivity
import com.maxholmes.androidapp.screen.courtowner.register.RegisterCourtClusterActivity
import com.maxholmes.androidapp.screen.courtowner.statistics.StatisticActivity
import com.maxholmes.androidapp.screen.customer.home.adapter.CourtClusterCourtOwnerAdapter
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener

class HomeCourtOwnerActivity : AppCompatActivity() {

    private lateinit var courtClusterAdapter: CourtClusterCourtOwnerAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home_court_owner)

        // Set up RecyclerView and Adapter
        courtClusterAdapter = CourtClusterCourtOwnerAdapter()
        val recyclerView = findViewById<RecyclerView>(R.id.recyclerViewCourtCluster)
        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.adapter = courtClusterAdapter

        // Load fake data
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
        // Fake data for testing
        val fakeCourtClusters = mutableListOf(
            CourtCluster(
                id = "4f744841-3820-41c2-a301-2759072aeeda",
                name = "Sân Đức đẹp trai",
                openingTime = "05:00:00",
                closingTime = "22:00:00",
                description = null,
                addressId = "da04ee89-45e6-4e74-9ae0-51eb49b21610",
                courtOwnerId = "4aeab0f2-27f3-482f-a2eb-07875b741a9a",
                imageUrl = null
            ),
            CourtCluster(
                id = "7ab447f8-2df9-4924-8fdc-2c7e17ed155f",
                name = "Sân của Đức",
                openingTime = "05:00:00",
                closingTime = "22:00:00",
                description = null,
                addressId = "6d7f66e0-e83e-46cb-8c70-96e8de6e7192",
                courtOwnerId = "4aeab0f2-27f3-482f-a2eb-07875b741a9a",
                imageUrl = null
            ),
            CourtCluster(
                id = "8cf22525-7219-4970-a038-93092525b528",
                name = "Sân Đức siêu đẹp trai",
                openingTime = "05:00:00",
                closingTime = "22:00:00",
                description = null,
                addressId = "da04ee89-45e6-4e74-9ae0-51eb49b21610",
                courtOwnerId = "4aeab0f2-27f3-482f-a2eb-07875b741a9a",
                imageUrl = null
            ),
            CourtCluster(
                id = "cd57ab9e-44a5-42a9-b674-176074c51d03",
                name = "Sân Pickleball Hồ Gươm",
                openingTime = "06:00:00",
                closingTime = "22:00:00",
                description = "Sân pickleball hiện đại với mặt sân tiêu chuẩn quốc tế, phù hợp cho cả người chơi mới và chuyên nghiệp.",
                addressId = "fbc9ff58-6136-40ad-93a4-90fcb301f4a2",
                courtOwnerId = "4aeab0f2-27f3-482f-a2eb-07875b741a9a",
                imageUrl = null
            )
        )

        // Update RecyclerView
        courtClusterAdapter.updateData(fakeCourtClusters)
    }
}


//package com.maxholmes.androidapp.screen.courtowner.home
//
//import android.content.Intent
//import android.os.Bundle
//import android.widget.Toast
//import androidx.appcompat.app.AppCompatActivity
//import androidx.recyclerview.widget.LinearLayoutManager
//import com.maxholmes.androidapp.R
//import com.maxholmes.androidapp.data.dto.response.APIResponse
//import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
//import com.maxholmes.androidapp.data.model.CourtCluster
//import com.maxholmes.androidapp.data.service.RetrofitClient
//import com.maxholmes.androidapp.screen.courtowner.detail.CourtClusterEditActivity
//import com.maxholmes.androidapp.screen.courtowner.register.RegisterCourtClusterActivity
//import com.maxholmes.androidapp.screen.courtowner.statistics.StatisticActivity
//import com.maxholmes.androidapp.screen.customer.home.adapter.CourtClusterCourtOwnerAdapter
//import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
//import retrofit2.Call
//import retrofit2.Callback
//import retrofit2.Response
//
//class CourtOwnerHomeActivity : AppCompatActivity() {
//
//    private lateinit var courtClusterAdapter: CourtClusterCourtOwnerAdapter
//
//    override fun onCreate(savedInstanceState: Bundle?) {
//        super.onCreate(savedInstanceState)
//        setContentView(R.layout.activity_home_court_owner)
//
//        // Set up RecyclerView and Adapter
//        courtClusterAdapter = CourtClusterCourtOwnerAdapter()
//        val recyclerView = findViewById<androidx.recyclerview.widget.RecyclerView>(R.id.recyclerViewCourtCluster)
//        recyclerView.layoutManager = LinearLayoutManager(this)
//        recyclerView.adapter = courtClusterAdapter
//
//        fetchCourtClusters()
//
//        // Handle item click events
//        courtClusterAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtCluster> {
//            override fun onItemClick(item: CourtCluster?) {
//                item?.let {
//                    // Navigate to edit screen
//                    val intent = Intent(this@CourtOwnerHomeActivity, CourtClusterEditActivity::class.java)
//                    intent.putExtra("courtClusterId", it.id)
//                    startActivity(intent)
//                }
//            }
//        })
//
//        // Set up back button
//        val backButton = findViewById<android.widget.ImageButton>(R.id.backButton)
//        backButton.setOnClickListener { finish() }
//
//        // Set up statistics button
//        val btnStats = findViewById<android.widget.Button>(R.id.btn_stats)
//        btnStats.setOnClickListener {
//            val intent = Intent(this@CourtOwnerHomeActivity, StatisticActivity::class.java)
//            startActivity(intent)
//        }
//
//        // Set up add new button
//        val btnAddNew = findViewById<android.widget.Button>(R.id.btn_add_new)
//        btnAddNew.setOnClickListener {
//            val intent = Intent(this@CourtOwnerHomeActivity, RegisterCourtClusterActivity::class.java)
//            startActivity(intent)
//        }
//    }
//
//    private fun fetchCourtClusters() {
//        RetrofitClient.ApiClient.apiService.getAllCourtClusters().enqueue(object : Callback<APIResponse> {
//            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
//                if (response.isSuccessful) {
//                    response.body()?.let { apiResponse ->
//                        val courtClusters: List<CourtCluster>? = parseApiResponseData(apiResponse.data)
//                        courtClusters?.let {
//                            courtClusterAdapter.updateData(it.toMutableList())
//                        }
//                    }
//                } else {
//                    // Handle unsuccessful response
//                    Toast.makeText(this@CourtOwnerHomeActivity, "Failed to load data", Toast.LENGTH_SHORT).show()
//                }
//            }
//
//            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
//                // Handle failure, e.g., no internet connection
//                Toast.makeText(this@CourtOwnerHomeActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
//            }
//        })
//    }
//}