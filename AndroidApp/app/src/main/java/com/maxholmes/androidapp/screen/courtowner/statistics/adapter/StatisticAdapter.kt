package com.maxholmes.androidapp.screen.courtowner.statistics.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.StatisticDto
import com.maxholmes.androidapp.databinding.ItemStatisticBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener

class StatisticAdapter : RecyclerView.Adapter<StatisticAdapter.ViewHolder>() {
    private val statistics = mutableListOf<StatisticDto>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<StatisticDto>? = null

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int,
    ): ViewHolder {
        val binding = ItemStatisticBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(
        holder: ViewHolder,
        position: Int,
    ) {
        holder.bindViewData(statistics[position])
    }

    override fun getItemCount(): Int {
        return statistics.size
    }

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<StatisticDto>) {
        onItemClickListener = listener
    }

    fun updateData(statistics: MutableList<StatisticDto>?) {
        statistics?.let {
            this.statistics.clear()
            this.statistics.addAll(it)
            notifyDataSetChanged()
        }
    }

    class ViewHolder(
        private val binding: ItemStatisticBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<StatisticDto>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var statisticData: StatisticDto? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(statistic: StatisticDto) {
            statisticData = statistic

            // Set court name
            binding.tvCourtName.text = statistic.courtCluster?.name ?: "N/A"

            binding.tvRevenue.text = "Doanh thu: ${statistic.totalRevenue}₫"

            binding.tvBookings.text = "Số lượt đặt sân: ${statistic.totalBookings}"
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(statisticData)
        }
    }
}
