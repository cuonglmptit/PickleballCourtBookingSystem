package com.maxholmes.androidapp.screen.customer.booking.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.databinding.ItemSelectDayBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.getDayOfMonth
import com.maxholmes.androidapp.utils.ext.getDayOfWeekName
import java.util.Calendar

class SelectDayAdapter : RecyclerView.Adapter<SelectDayAdapter.ViewHolder>() {
    private val selectDays = mutableListOf<Calendar>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<Calendar>? = null
    private var selectedItemPosition: Int = RecyclerView.NO_POSITION

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val binding = ItemSelectDayBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.bindViewData(selectDays[position], position == selectedItemPosition)
    }

    override fun getItemCount(): Int {
        return selectDays.size
    }

    fun getSelectDays(): List<Calendar> {
        return selectDays
    }

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<Calendar>) {
        onItemClickListener = listener
    }

    fun updateData(selectDays: MutableList<Calendar>?) {
        selectDays?.let {
            this.selectDays.clear()
            this.selectDays.addAll(it)
            notifyDataSetChanged()
        }
    }

    fun setSelectedItem(position: Int) {
        val previousPosition = selectedItemPosition
        selectedItemPosition = position
        if (previousPosition != RecyclerView.NO_POSITION) {
            notifyItemChanged(previousPosition)
        }
        notifyItemChanged(selectedItemPosition)
    }


    class ViewHolder(
        private val binding: ItemSelectDayBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<Calendar>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var selectDayData: Calendar? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(selectDay: Calendar, isSelected: Boolean) {
            selectDayData = selectDay
            binding.dayOfWeekTextView.text = selectDay.getDayOfWeekName()
            binding.dayTextView.text = selectDay.getDayOfMonth().toString()
            binding.root.setBackgroundColor(
                if (isSelected) binding.root.context.getColor(R.color.yellow) else binding.root.context.getColor(
                    R.color.white
                )
            )
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(selectDayData)
        }
    }
}
