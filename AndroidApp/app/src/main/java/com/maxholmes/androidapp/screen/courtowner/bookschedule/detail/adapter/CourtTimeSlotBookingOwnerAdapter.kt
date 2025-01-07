package com.maxholmes.androidapp.screen.customer.bookschedule.detail

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.databinding.ItemCourtTimeSlotCustomBookingBinding
import com.maxholmes.androidapp.utils.ext.toCustomDateFormat

class CourtTimeSlotCourtOwnerBookingAdapter(private val timeSlots: List<CourtTimeSlot>) :
    RecyclerView.Adapter<CourtTimeSlotCourtOwnerBookingAdapter.CourtTimeSlotViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CourtTimeSlotViewHolder {
        val binding = ItemCourtTimeSlotCustomBookingBinding.inflate(
            LayoutInflater.from(parent.context), parent, false
        )
        return CourtTimeSlotViewHolder(binding)
    }

    override fun onBindViewHolder(holder: CourtTimeSlotViewHolder, position: Int) {
        val timeSlot = timeSlots[position]
        holder.bind(timeSlot)
    }

    override fun getItemCount(): Int = timeSlots.size

    class CourtTimeSlotViewHolder(private val binding: ItemCourtTimeSlotCustomBookingBinding) :
        RecyclerView.ViewHolder(binding.root) {

        fun bind(timeSlot: CourtTimeSlot) {
            binding.dateText.text = timeSlot.date.toCustomDateFormat()
            binding.timeText.text = timeSlot.time
            binding.priceText.text = "${timeSlot.price} đ"
        }
    }
}
