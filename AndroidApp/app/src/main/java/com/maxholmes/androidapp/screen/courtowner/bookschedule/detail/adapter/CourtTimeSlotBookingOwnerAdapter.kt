package com.maxholmes.androidapp.screen.customer.bookschedule.detail

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtTimeSlot

class CourtTimeSlotCourtOwnerBookingAdapter(private val timeSlots: List<CourtTimeSlot>) :
    RecyclerView.Adapter<CourtTimeSlotCourtOwnerBookingAdapter.CourtTimeSlotViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CourtTimeSlotViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.item_court_time_slot_custom_booking, parent, false)
        return CourtTimeSlotViewHolder(view)
    }

    override fun onBindViewHolder(holder: CourtTimeSlotViewHolder, position: Int) {
        val timeSlot = timeSlots[position]
        holder.tvTime.text = timeSlot.time
        holder.tvPrice.text = "${timeSlot.price} đ"
    }

    override fun getItemCount(): Int {
        return timeSlots.size
    }

    class CourtTimeSlotViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val tvTime: TextView = itemView.findViewById(R.id.tvTime)
        val tvPrice: TextView = itemView.findViewById(R.id.tvPrice)
    }
}
