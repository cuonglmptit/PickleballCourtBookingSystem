package com.maxholmes.androidapp.screen.customer.bookschedule.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.databinding.ItemBookingScheduleBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.enum.BookingStatusEnum
import com.maxholmes.androidapp.utils.ext.toCustomDateFormat
import com.maxholmes.androidapp.utils.ext.toCustomDateTimeFormat
import com.pickleballcourtbookingsystem.api.dtos.CustomBookingResponse

class CustomBookingAdapter : RecyclerView.Adapter<CustomBookingAdapter.ViewHolder>() {

    private val customBookings = mutableListOf<CustomBookingResponse>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<CustomBookingResponse>? = null

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int,
    ): ViewHolder {
        val binding = ItemBookingScheduleBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(
        holder: ViewHolder,
        position: Int,
    ) {
        holder.bindViewData(customBookings[position])
    }

    override fun getItemCount(): Int {
        return customBookings.size
    }

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<CustomBookingResponse>) {
        onItemClickListener = listener
    }

    fun updateData(bookings: MutableList<CustomBookingResponse>?) {
        bookings?.let {
            this.customBookings.clear()
            this.customBookings.addAll(it)
            notifyDataSetChanged()
        }
    }

    class ViewHolder(
        private val binding: ItemBookingScheduleBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<CustomBookingResponse>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {

        private var bookingData: CustomBookingResponse? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(booking: CustomBookingResponse) {
            bookingData = booking
            val lastUpdateTime = "Thời gian cập nhật cuối: ${booking?.lastUpdatedTime!!.toCustomDateTimeFormat()}"


            var dateUse = "Ngày dùng sân: ${booking?.courtTimeSlots?.get(0)?.date!!.toCustomDateFormat()}"
            if(booking.booking!!.status == BookingStatusEnum.Canceled)
            {
                dateUse = "Thời gian đặt sân: ${booking?.booking.timeBooking.toCustomDateTimeFormat()}"
            }
            binding.tvCourtClusterName.text = "Cụm sân: ${booking.courtClusterName}"
            binding.tvAddress.text = booking.address.toString()
            binding.tvTimeBooking.text = lastUpdateTime
            binding.tvDateUse.text = dateUse
            binding.tvStatusValue.text = when (booking.booking!!.status) {
                BookingStatusEnum.Pending -> "Đang chờ xử lý"
                BookingStatusEnum.CourtOwnerConfirmed -> "Đã được xác nhận"
                BookingStatusEnum.Canceled -> "Đã hủy"
                BookingStatusEnum.All -> "Tất cả trạng thái"
            }
            binding.tvPriceValue.text = "${booking.booking?.amount?.toInt()} đ"
            binding.tvPhone.text = booking.courtOwnerPhoneNumber ?: "Không có số điện thoại"
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(bookingData)
        }
    }
}
