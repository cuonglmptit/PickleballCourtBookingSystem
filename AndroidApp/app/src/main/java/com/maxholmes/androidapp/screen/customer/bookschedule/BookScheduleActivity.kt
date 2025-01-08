package com.maxholmes.androidapp.screen.customer.bookschedule

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
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.customer.booking.BookingActivity
import com.maxholmes.androidapp.screen.customer.bookschedule.adapter.CustomBookingAdapter
import com.maxholmes.androidapp.screen.customer.bookschedule.detail.BookingCustomerDetailActivity
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.screen.customer.search.SearchActivity
import com.maxholmes.androidapp.screen.user.UserActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.SpacingItemDecoration
import com.maxholmes.androidapp.utils.ext.authentication.getRoleFromToken
import com.maxholmes.androidapp.utils.ext.parseToLocalDateTime
import com.pickleballcourtbookingsystem.api.dtos.CustomBookingResponse
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class BookScheduleActivity : AppCompatActivity() {

    private lateinit var binding: ActivityBookScheduleBinding
    private lateinit var customBookingAdapter: CustomBookingAdapter
    private val bookings = mutableListOf<CustomBookingResponse>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBookScheduleBinding.inflate(layoutInflater)
        setContentView(binding.root)
        val token = SharedPreferencesUtils.getToken(this)

        customBookingAdapter = CustomBookingAdapter()
        binding.recyclerViewBookings.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewBookings.adapter = customBookingAdapter
        val spacingInDp = resources.getDimensionPixelSize(R.dimen.dp_10)
        binding.recyclerViewBookings.addItemDecoration(SpacingItemDecoration(spacingInDp))
        if(token != null)
        {
            setupBottomNavigation(token)
        }

        fetchBookings()
        binding.bottomNavigationView.selectedItemId = R.id.booking

        customBookingAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CustomBookingResponse> {
            override fun onItemClick(item: CustomBookingResponse?) {
                item?.let {
                    val intent = Intent(this@BookScheduleActivity, BookingCustomerDetailActivity::class.java)
                    intent.putExtra("bookingDetail", it)
                    startActivity(intent)
                }
            }
        })

    }

    private fun fetchBookings() {
        val token = SharedPreferencesUtils.getToken(this)
        val headerAuthorize = "Bearer $token"

        RetrofitClient.ApiClient.apiService.getBookingByUser(headerAuthorize).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val parsedBookings: List<CustomBookingResponse>? = parseApiResponseData(apiResponse.data)
                        if (!parsedBookings.isNullOrEmpty()) {
                            val sortedBookings = parsedBookings.sortedByDescending {booking -> booking.lastUpdatedTime.parseToLocalDateTime()}
                            bookings.clear()
                            bookings.addAll(sortedBookings)
                            customBookingAdapter.updateData(bookings)
                        } else {
                            Toast.makeText(this@BookScheduleActivity, "Không có lịch đặt", Toast.LENGTH_SHORT).show()
                        }
                    }
                } else {
                    Toast.makeText(this@BookScheduleActivity, "Lỗi khi lấy dữ liệu", Toast.LENGTH_SHORT).show()
                    Log.e("BookScheduleActivity", "Error: ${response.message()}")
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@BookScheduleActivity, "Không thể kết nối đến server", Toast.LENGTH_SHORT).show()
                Log.e("BookScheduleActivity", "Failure: ${t.message}")
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
