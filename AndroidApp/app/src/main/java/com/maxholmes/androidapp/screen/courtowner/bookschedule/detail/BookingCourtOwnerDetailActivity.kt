package com.maxholmes.androidapp.screen.courtowner.bookschedule.detail

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.request.CancelBookingRequest
import com.maxholmes.androidapp.data.dto.request.ConfirmBookingRequest
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.model.Booking
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.screen.courtowner.bookschedule.BookScheduleOwnerActivity
import com.maxholmes.androidapp.utils.enum.BookingStatusEnum
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.databinding.ActivityBookingCourtOwnerDetailBinding
import com.maxholmes.androidapp.screen.customer.bookschedule.detail.CourtTimeSlotBookingDetailAdapter
import com.maxholmes.androidapp.screen.customer.bookschedule.detail.CourtTimeSlotCourtOwnerBookingAdapter
import com.maxholmes.androidapp.utils.ext.toCustomDateFormat
import com.maxholmes.androidapp.utils.ext.toCustomDateTimeFormat
import com.pickleballcourtbookingsystem.api.dtos.CustomBookingResponse
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class BookingCourtOwnerDetailActivity : AppCompatActivity() {

    private lateinit var binding: ActivityBookingCourtOwnerDetailBinding
    private var customBookingResponse: CustomBookingResponse? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBookingCourtOwnerDetailBinding.inflate(layoutInflater)
        setContentView(binding.root)

        customBookingResponse = intent.getParcelableExtra("bookingDetail", CustomBookingResponse::class.java)
        val token = SharedPreferencesUtils.getToken(this)

        customBookingResponse?.let { booking ->
            val courtClusterName = "Tên cụm sân: ${customBookingResponse!!.courtClusterName}"
            binding.tvCourtClusterName.text = courtClusterName
            binding.tvAddress.text = booking.address?.toString()
            var lastUpdateTime = "Thời gian đặt sân: ${customBookingResponse?.lastUpdatedTime!!.toCustomDateTimeFormat()}"
            if(customBookingResponse?.booking!!.status == BookingStatusEnum.Canceled)
            {
                lastUpdateTime = "Thời gian hủy sân: ${customBookingResponse?.lastUpdatedTime!!.toCustomDateTimeFormat()}"
            }
            var dateUse = "Ngày dùng sân: ${customBookingResponse?.courtTimeSlots?.get(0)?.date!!.toCustomDateFormat()}"
            if(customBookingResponse?.booking!!.status == BookingStatusEnum.Canceled)
            {
                dateUse = "Ngày dùng sân: ${customBookingResponse!!.booking!!.timeBooking.toCustomDateFormat()}"
            }
            val customerPhoneNumber: String = "Số điện thoại khách hàng: ${customBookingResponse!!.customerPhoneNumber}"
            binding.tvPhone.text = customerPhoneNumber
            val status = when (customBookingResponse!!.booking!!.status) {
                BookingStatusEnum.Pending -> "Đang chờ xử lý"
                BookingStatusEnum.CourtOwnerConfirmed -> "Đã được chủ sân xác nhận"
                BookingStatusEnum.Canceled -> "Đã hủy"
                BookingStatusEnum.Completed -> "Đã thanh toán"
                BookingStatusEnum.All -> "Tất cả trạng thái"
            }
            val statusText = "Trạng thái: $status"
            binding.tvStatusValue.text = statusText
            binding.tvTimeBooking.text = lastUpdateTime
            binding.tvDateUse.text = dateUse
            binding.tvPriceValue.text = "Tổng giá tiền: ${booking.booking?.amount}"

            when (booking.booking?.status) {
                BookingStatusEnum.Pending -> {
                    binding.cancelButton.visibility = Button.VISIBLE
                    binding.confirmBookingButton.visibility = Button.VISIBLE
                    binding.confirmPaymentButton.visibility = Button.GONE
                }
                BookingStatusEnum.CourtOwnerConfirmed -> {
                    binding.cancelButton.visibility = Button.GONE
                    binding.confirmBookingButton.visibility = Button.GONE
                    binding.confirmPaymentButton.visibility = Button.VISIBLE
                }
                BookingStatusEnum.Canceled -> {
                    binding.cancelButton.visibility = Button.GONE
                    binding.confirmBookingButton.visibility = Button.GONE
                    binding.confirmPaymentButton.visibility = Button.GONE
                }
                BookingStatusEnum.Completed -> {
                    binding.cancelButton.visibility = Button.GONE
                    binding.confirmBookingButton.visibility = Button.GONE
                    binding.confirmPaymentButton.visibility = Button.GONE
                }
                else -> {
                    binding.cancelButton.visibility = Button.GONE
                    binding.confirmBookingButton.visibility = Button.GONE
                    binding.confirmPaymentButton.visibility = Button.GONE
                }
            }

            binding.recyclerViewTimeSlots.layoutManager = LinearLayoutManager(this)
            val sortedTimeSlots = customBookingResponse?.courtTimeSlots?.sortedBy { it.time } ?: listOf()
            val timeSlotAdapter = CourtTimeSlotBookingDetailAdapter(sortedTimeSlots)
            binding.recyclerViewTimeSlots.adapter = timeSlotAdapter


            binding.cancelButton.setOnClickListener {
                if (token != null) {
                    cancelBooking(token, booking.booking)
                }
            }

            binding.confirmBookingButton.setOnClickListener {
                if (token != null) {
                    confirmBooking(token, booking.booking)
                }
            }

            binding.confirmPaymentButton.setOnClickListener {
                if (token != null) {
                    confirmPayment(token, booking.booking)
                }
            }
            binding.backButton.setOnClickListener {
                onBackPressedDispatcher.onBackPressed()
            }

        }
    }

    private fun cancelBooking(token: String, booking: Booking?) {
        booking?.let {
            val cancelBookingRequest = CancelBookingRequest(bookingId = it.id)
            val tokenHeader = "Bearer $token"
            RetrofitClient.ApiClient.apiService.cancelBooking(cancelBookingRequest, tokenHeader)
                .enqueue(object : Callback<APIResponse> {
                    override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                        if (response.isSuccessful) {
                            Toast.makeText(this@BookingCourtOwnerDetailActivity, "Đã hủy booking thành công!", Toast.LENGTH_SHORT).show()
                            val intent = Intent(this@BookingCourtOwnerDetailActivity, BookScheduleOwnerActivity::class.java)
                            startActivity(intent)
                            finish()
                        } else {
                            Toast.makeText(this@BookingCourtOwnerDetailActivity, "Lỗi khi hủy booking!", Toast.LENGTH_SHORT).show()
                        }
                    }

                    override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                        Toast.makeText(this@BookingCourtOwnerDetailActivity, "Lỗi kết nối!", Toast.LENGTH_SHORT).show()
                    }
                })
        }
    }

    private fun confirmBooking(token: String, booking: Booking?) {
        booking?.let {
            val tokenHeader = "Bearer $token"
            val confirmBookingRequest = ConfirmBookingRequest(bookingId = it.id)
            RetrofitClient.ApiClient.apiService.courtOwnerConfirmBooking(confirmBookingRequest, tokenHeader)
                .enqueue(object : Callback<APIResponse> {
                    override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                        if (response.isSuccessful) {
                            Toast.makeText(this@BookingCourtOwnerDetailActivity, "Đã xác nhận booking!", Toast.LENGTH_SHORT).show()
                            val intent = Intent(this@BookingCourtOwnerDetailActivity, BookScheduleOwnerActivity::class.java)
                            startActivity(intent)
                            finish()
                        } else {
                            Toast.makeText(this@BookingCourtOwnerDetailActivity, "Lỗi khi xác nhận booking!", Toast.LENGTH_SHORT).show()
                        }
                    }

                    override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                        Toast.makeText(this@BookingCourtOwnerDetailActivity, "Lỗi kết nối!", Toast.LENGTH_SHORT).show()
                    }
                })
        }
    }

    private fun confirmPayment(token: String, booking: Booking?) {
        booking?.let {
            val tokenHeader = "Bearer $token"
            val confirmPaymentRequest = ConfirmBookingRequest(bookingId = it.id)
            RetrofitClient.ApiClient.apiService.courtOwnerConfirmPaid(confirmPaymentRequest, tokenHeader)
                .enqueue(object : Callback<APIResponse> {
                    override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                        if (response.isSuccessful) {
                            Toast.makeText(this@BookingCourtOwnerDetailActivity, "Thanh toán đã được xác nhận!", Toast.LENGTH_SHORT).show()
                            val intent = Intent(this@BookingCourtOwnerDetailActivity, BookScheduleOwnerActivity::class.java)
                            startActivity(intent)
                            finish()
                        } else {
                            Toast.makeText(this@BookingCourtOwnerDetailActivity, "Lỗi khi xác nhận thanh toán!", Toast.LENGTH_SHORT).show()
                        }
                    }

                    override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                        Toast.makeText(this@BookingCourtOwnerDetailActivity, "Lỗi kết nối!", Toast.LENGTH_SHORT).show()
                    }
                })
        }
    }
}
