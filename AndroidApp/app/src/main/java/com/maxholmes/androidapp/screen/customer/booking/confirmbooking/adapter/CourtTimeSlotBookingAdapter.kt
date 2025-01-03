package com.example.yourapp.screen.customer.booking.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.databinding.ItemCourtTimeSlotBookingBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import java.time.LocalTime
import java.time.format.DateTimeFormatter

class CourtTimeSlotBookingAdapter : RecyclerView.Adapter<CourtTimeSlotBookingAdapter.ViewHolder>() {

    private val timeSlots = mutableListOf<CourtTimeSlot>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<CourtTimeSlot>? = null

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val binding = ItemCourtTimeSlotBookingBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.bindViewData(timeSlots[position])
    }

    override fun getItemCount(): Int = timeSlots.size

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<CourtTimeSlot>) {
        onItemClickListener = listener
    }

    fun updateData(newTimeSlots: List<CourtTimeSlot>) {
        timeSlots.clear()
        timeSlots.addAll(newTimeSlots)
        notifyDataSetChanged()
    }

    class ViewHolder(
        private val binding: ItemCourtTimeSlotBookingBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<CourtTimeSlot>?
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {

        private var currentData: CourtTimeSlot? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(timeSlot: CourtTimeSlot) {
            val startTime = LocalTime.parse(timeSlot.time, DateTimeFormatter.ofPattern("HH:mm"))
            val endTime = startTime.plusHours(1)
            val timeRange = "${startTime.format(DateTimeFormatter.ofPattern("HH:mm"))} - ${endTime.format(DateTimeFormatter.ofPattern("HH:mm"))}"
            binding.dateText.text = timeSlot.date
            binding.timeText.text = timeRange
            binding.courtNumberText.text = "Sân 1"
            binding.priceText.text = "${timeSlot.price}₫"
        }

        override fun onClick(v: View?) {
            itemClickListener?.onItemClick(currentData)
        }
    }
}
