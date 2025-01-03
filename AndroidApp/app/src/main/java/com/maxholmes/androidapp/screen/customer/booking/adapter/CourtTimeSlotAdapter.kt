package com.maxholmes.androidapp.screen.customer.booking.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.databinding.ItemSelectTimeSlotBinding
import com.maxholmes.androidapp.screen.customer.booking.BookingActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import java.time.LocalTime
import java.time.format.DateTimeFormatter

class CourtTimeSlotAdapter : RecyclerView.Adapter<CourtTimeSlotAdapter.ViewHolder>() {
    private val timeSlots = mutableListOf<CourtTimeSlot>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<CourtTimeSlot>? = null
    private val selectedPositions = mutableSetOf<Int>() // Set để lưu vị trí các phần tử đã chọn

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val binding = ItemSelectTimeSlotBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.bindViewData(timeSlots[position], selectedPositions.contains(position)) // Kiểm tra phần tử có được chọn không
    }

    override fun getItemCount(): Int {
        return timeSlots.size
    }

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<CourtTimeSlot>) {
        onItemClickListener = listener
    }

    fun updateData(timeSlots: MutableList<CourtTimeSlot>) {
        this.timeSlots.clear()
        this.timeSlots.addAll(timeSlots)
        notifyDataSetChanged()
    }

    // Thêm hoặc giữ trạng thái đã chọn
    fun toggleSelection(position: Int) {
        if (selectedPositions.contains(position)) {
            // Nếu đã chọn, giữ nguyên trạng thái
            // Không làm gì ở đây vì yêu cầu giữ nguyên
        } else {
            selectedPositions.add(position) // Thêm vào set
            notifyItemChanged(position) // Làm mới giao diện của phần tử được chọn
        }
    }

    fun getCourtTimeSlots(): List<CourtTimeSlot> {
        return timeSlots
    }


    class ViewHolder(
        private val binding: ItemSelectTimeSlotBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<CourtTimeSlot>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var timeSlotData: CourtTimeSlot? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(timeSlot: CourtTimeSlot, isSelected: Boolean) {
            timeSlotData = timeSlot

            val startTime = LocalTime.parse(timeSlot.time, DateTimeFormatter.ofPattern("HH:mm"))
            val endTime = startTime.plusHours(1)
            val timeRange = "${startTime.format(DateTimeFormatter.ofPattern("HH:mm"))} - ${endTime.format(DateTimeFormatter.ofPattern("HH:mm"))}"

            binding.timeTextView.text = timeRange
            binding.priceTextView.text = "${timeSlot.price} VND"

            // Thay đổi màu nền theo trạng thái được chọn
            binding.root.setBackgroundColor(
                if (isSelected) binding.root.context.getColor(R.color.yellow) else binding.root.context.getColor(
                    R.color.white
                )
            )
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(timeSlotData)
            (view?.context as? BookingActivity)?.courtTimeSlotAdapter?.toggleSelection(adapterPosition) // Thêm trạng thái chọn
        }
    }
}
