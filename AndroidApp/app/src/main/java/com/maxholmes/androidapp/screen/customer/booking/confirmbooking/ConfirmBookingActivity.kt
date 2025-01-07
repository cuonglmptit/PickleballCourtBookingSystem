package com.maxholmes.androidapp.screen.customer.booking.confirmbooking

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.yourapp.screen.customer.booking.adapter.CourtTimeSlotBookingAdapter
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.request.AddBookingRequest
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.CourtOwnerInfoDto
import com.maxholmes.androidapp.data.dto.response.CustomerInfoDto
import com.maxholmes.androidapp.data.dto.response.UserInfoDto
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.toCustomDateFormat

import com.maxholmes.androidapp.databinding.ActivityConfirmBookingBinding
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ConfirmBookingActivity : AppCompatActivity() {

    private lateinit var binding: ActivityConfirmBookingBinding
    private lateinit var courtTimeSlotBookingAdapter: CourtTimeSlotBookingAdapter
    private var courtCluster: CourtCluster? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        println("Da vao")
        binding = ActivityConfirmBookingBinding.inflate(layoutInflater)
        setContentView(binding.root)
        val courtTimeSlots = intent.getParcelableArrayListExtra("selectedTimeSlots", CourtTimeSlot::class.java)!!.toMutableList()
        val selectedCourt = intent.getParcelableExtra("selectedCourt", Court::class.java)
        courtCluster = intent.getParcelableExtra("courtCluster", CourtCluster::class.java)
        val courtName = "Sân ${selectedCourt?.courtNumber}"
        loadCustomerInfo()
        loadCourtOwnerInfo()
        courtTimeSlotBookingAdapter = CourtTimeSlotBookingAdapter()
        val totalPrice = "${courtTimeSlots.sumOf { it.price }} ₫"
        binding.recyclerViewTimeSlots.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewTimeSlots.adapter = courtTimeSlotBookingAdapter
        courtTimeSlotBookingAdapter.updateData(courtTimeSlots)

        courtTimeSlotBookingAdapter.registerItemRecyclerViewClickListener(object :
            OnItemRecyclerViewClickListener<CourtTimeSlot> {
            override fun onItemClick(item: CourtTimeSlot?) {
                item?.let {
                    Toast.makeText(
                        this@ConfirmBookingActivity,
                        "Đã chọn: ${item.time} - ${item.price}",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }
        })
        binding.courtClusterName.text = courtCluster!!.name
        binding.courtAddress.text = courtCluster!!.address.toString()
        binding.courtNumberText.text = courtName
        binding.totalPrice.text = totalPrice
        binding.bookButton.setOnClickListener {
            onBookButtonClick()
        }
        binding.backButton.setOnClickListener {
            onBackPressedDispatcher.onBackPressed()
        }

    }

    private fun loadCourtOwnerInfo() {
        val token = SharedPreferencesUtils.getToken(this)
        if (token.isNullOrEmpty()) {
            Toast.makeText(this@ConfirmBookingActivity, "Token không hợp lệ!", Toast.LENGTH_SHORT).show()
            return
        }
        val tokenHeader = "Bearer $token"

        RetrofitClient.ApiClient.apiService.getCourtOwnerInfo(courtCluster!!.courtOwnerId, tokenHeader).enqueue(object :
            Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        println("Da vao day roi")
                        val courtOwnerInfo : CourtOwnerInfoDto? = parseApiResponseData(apiResponse.data)
                        println(courtOwnerInfo?.name + "Hello")
                        courtOwnerInfo?.let {
                            binding.courtOwnerPhoneNumber.text = it.phoneNumber
                        }
                    }
                } else {
                    Toast.makeText(this@ConfirmBookingActivity, "Lỗi khi tải thông tin chủ sân!", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@ConfirmBookingActivity, "Lỗi kết nối mạng. Vui lòng thử lại!", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun loadCustomerInfo() {
        val token = SharedPreferencesUtils.getToken(this)
        if (token.isNullOrEmpty()) {
            Toast.makeText(this@ConfirmBookingActivity, "Token không hợp lệ!", Toast.LENGTH_SHORT).show()
            return
        }
        val tokenHeader = "Bearer $token"
        println("Token la: $tokenHeader")
        RetrofitClient.ApiClient.apiService.getUserInfo(tokenHeader).enqueue(object :
            Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val customerInfoDto : UserInfoDto? = parseApiResponseData(apiResponse.data)
                        customerInfoDto?.let {
                            binding.customerName.text = it.name
                            binding.customerPhone.text = it.phoneNumber
                            binding.customerEmail.text = it.email
                        }
                    }
                } else {
                Toast.makeText(this@ConfirmBookingActivity, "Lỗi khi tải thông tin khách hàng!", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@ConfirmBookingActivity, "Lỗi kết nối mạng. Vui lòng thử lại!", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun onBookButtonClick() {
        val selectedTimeSlots = intent.getParcelableArrayListExtra("selectedTimeSlots", CourtTimeSlot::class.java)!!.toMutableList()
        val selectedCourt = intent.getParcelableExtra("selectedCourt", Court::class.java)
        val selectedTimeSlotIds = selectedTimeSlots.map { it.id }

        val addBookingRequest = AddBookingRequest(
            courtTimeSlotsIds = selectedTimeSlotIds,
            courtId = selectedCourt!!.id
        )

        val token = SharedPreferencesUtils.getToken(this)
        if (token.isNullOrEmpty()) {
            Toast.makeText(this, "Token không hợp lệ!", Toast.LENGTH_SHORT).show()
            return
        }
        val tokenHeader = "Bearer $token"

        RetrofitClient.ApiClient.apiService.addBooking(addBookingRequest, tokenHeader).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    Toast.makeText(this@ConfirmBookingActivity, "Đặt sân thành công!", Toast.LENGTH_SHORT).show()
                    val intent = Intent(this@ConfirmBookingActivity, HomeCustomerActivity::class.java)
                    startActivity(intent)
                    finish()
                } else {
                    Toast.makeText(this@ConfirmBookingActivity, "Lỗi khi đặt sân. Vui lòng thử lại!", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@ConfirmBookingActivity, "Lỗi kết nối mạng. Vui lòng thử lại!", Toast.LENGTH_SHORT).show()
            }
        })
    }

}
