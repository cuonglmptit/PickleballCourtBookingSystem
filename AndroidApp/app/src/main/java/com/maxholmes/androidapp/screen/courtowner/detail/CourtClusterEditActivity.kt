package com.maxholmes.androidapp.screen.courtowner.detail

import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.databinding.ActivityCourtClusterEditBinding
import com.maxholmes.androidapp.screen.courtowner.add.AddCourtTimeSlotActivity
import com.maxholmes.androidapp.screen.courtowner.detail.courtprice.ManagePriceActivity
import com.maxholmes.androidapp.screen.courtowner.detail.statisticdetail.ViewStatsActivity
import com.maxholmes.androidapp.utils.ext.loadImageWithUrl


import android.content.Intent
import android.widget.Toast
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.ImageCourtUrl
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.utils.enum.CourtClusterStatus
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CourtClusterEditActivity : AppCompatActivity() {

    private lateinit var binding: ActivityCourtClusterEditBinding
    private var courtCluster: CourtCluster? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityCourtClusterEditBinding.inflate(layoutInflater)
        setContentView(binding.root)

        courtCluster = intent.getParcelableExtra("courtCluster", CourtCluster::class.java)

        if (courtCluster == null) {
            Toast.makeText(this, "Dữ liệu sân không hợp lệ!", Toast.LENGTH_SHORT).show()
            finish()
            return
        }

        updateUI()

        binding.backButton.setOnClickListener {
            onBackPressedDispatcher.onBackPressed()
        }

        binding.managePricesButton.setOnClickListener {
            val intent = Intent(this, ManagePriceActivity::class.java)
            intent.putExtra("courtClusterId", courtCluster!!.id)
            startActivity(intent)
        }

        binding.createSlotButton.setOnClickListener {
            val intent = Intent(this, AddCourtTimeSlotActivity::class.java)
            intent.putExtra("courtCluster", courtCluster)
            startActivity(intent)
        }

        binding.viewStatsButton.setOnClickListener {
            val intent = Intent(this, ViewStatsActivity::class.java)
            intent.putExtra("courtCluster", courtCluster)
            startActivity(intent)
        }
    }

    private fun updateUI() {
        binding.courtName.text = courtCluster?.name
        binding.timeText.text = "${courtCluster?.openingTime} - ${courtCluster?.closingTime}"
        binding.description.text = courtCluster?.description ?: "Không có mô tả"
        binding.addressText.text = courtCluster?.address.toString()
        if(courtCluster!!.status == CourtClusterStatus.Active.value) {
            binding.status.text = "Trạng thái: Hoạt động"
        }
        else
        {
            binding.status.text = "Trạng thái: Ngưng hoạt động"
        }


        RetrofitClient.ApiClient.apiService.getImagesByClusterId(courtCluster!!.id).enqueue(object: Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val images: List<ImageCourtUrl>? = parseApiResponseData(apiResponse.data)
                        if (images != null && images.size != 0)
                        {
                            binding.courtImage.loadImageWithUrl(images[0].url, R.drawable.image_court_1)
                        }
                        else
                        {
                            binding.courtImage.setImageResource(R.drawable.image_court_1)
                        }
                    }
                }
            }
            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
            }
        })
    }

}
