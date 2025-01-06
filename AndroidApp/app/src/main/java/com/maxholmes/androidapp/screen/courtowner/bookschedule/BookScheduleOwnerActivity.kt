package com.maxholmes.androidapp.screen.courtowner.bookschedule

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityBookScheduleBinding
import com.maxholmes.androidapp.screen.courtowner.bookschedule.adapter.CustomBookingCourtOwnerAdapter
import com.maxholmes.androidapp.screen.courtowner.bookschedule.detail.BookingCourtOwnerDetailActivity
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.customer.bookschedule.detail.BookingCustomerDetailActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.SpacingItemDecoration
import com.maxholmes.androidapp.utils.ext.parseToLocalDateTime
import com.pickleballcourtbookingsystem.api.dtos.CustomBookingResponse
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class BookScheduleOwnerActivity : AppCompatActivity() {

    private lateinit var binding: ActivityBookScheduleBinding
    private lateinit var customBookingAdapter: CustomBookingCourtOwnerAdapter
    private val bookings = mutableListOf<CustomBookingResponse>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBookScheduleBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val token = SharedPreferencesUtils.getToken(this)
        customBookingAdapter = CustomBookingCourtOwnerAdapter()

        binding.recyclerViewBookings.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewBookings.adapter = customBookingAdapter
        val spacingInDp = resources.getDimensionPixelSize(R.dimen.dp_10)
        binding.recyclerViewBookings.addItemDecoration(SpacingItemDecoration(spacingInDp))

        if (token != null) {
            setupBottomNavigation(token)
        }

        fetchBookings()
        binding.bottomNavigationView.selectedItemId = R.id.booking

        customBookingAdapter.registerItemRecyclerViewClickListener(object :
            OnItemRecyclerViewClickListener<CustomBookingResponse> {
            override fun onItemClick(item: CustomBookingResponse?) {
                item?.let {
                    val intent = Intent(this@BookScheduleOwnerActivity, BookingCourtOwnerDetailActivity::class.java)
                    intent.putExtra("bookingDetail", it)
                    startActivity(intent)
                }
            }
        })
    }

    private fun fetchBookings() {
        val token = SharedPreferencesUtils.getToken(this)
        val headerAuthorize = "Bearer $token"

        RetrofitClient.ApiClient.apiService.getBookingByUser(headerAuthorize)
            .enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val parsedBookings: List<CustomBookingResponse>? = parseApiResponseData(apiResponse.data)
                            if (!parsedBookings.isNullOrEmpty()) {
                                val sortedBookings = parsedBookings.sortedByDescending { booking -> parseToLocalDateTime(booking.lastUpdatedTime) }
                                bookings.clear()
                                bookings.addAll(sortedBookings)
                                customBookingAdapter.updateData(bookings)
                            } else {
                                Toast.makeText(this@BookScheduleOwnerActivity, "Không có lịch đặt", Toast.LENGTH_SHORT).show()
                            }
                        }
                    } else {
                        Toast.makeText(this@BookScheduleOwnerActivity, "Lỗi khi lấy dữ liệu", Toast.LENGTH_SHORT).show()
                        Log.e("BookScheduleOwnerActivity", "Error: ${response.message()}")
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    Toast.makeText(this@BookScheduleOwnerActivity, "Không thể kết nối đến server", Toast.LENGTH_SHORT).show()
                    Log.e("BookScheduleOwnerActivity", "Failure: ${t.message}")
                }
            })
    }

    private fun setupBottomNavigation(token: String) {
        binding.bottomNavigationView.setOnItemSelectedListener { item ->
            when (item.itemId) {
                R.id.home -> {
                    val intent = Intent(this, HomeCourtOwnerActivity::class.java)
                    startActivity(intent)
                    true
                }
                R.id.booking -> true
                else -> false
            }
        }
    }
}
