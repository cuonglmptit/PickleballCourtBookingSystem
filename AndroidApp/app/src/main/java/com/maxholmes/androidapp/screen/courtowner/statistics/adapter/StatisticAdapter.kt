package com.maxholmes.androidapp.screen.courtowner.statistics.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.Statistic
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.databinding.ItemStatisticBinding
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class StatisticAdapter : RecyclerView.Adapter<StatisticAdapter.ViewHolder>() {
    private val statistics = mutableListOf<Statistic>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<Statistic>? = null

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

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<Statistic>) {
        onItemClickListener = listener
    }

    fun updateData(statistics: MutableList<Statistic>?) {
        statistics?.let {
            this.statistics.clear()
            this.statistics.addAll(it)
            notifyDataSetChanged()
        }
    }

    class ViewHolder(
        private val binding: ItemStatisticBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<Statistic>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var statisticData: Statistic? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(statistic: Statistic) {
            statisticData = statistic

            // Load address dynamically
            RetrofitClient.ApiClient.apiService.getAddressById(statistic.courtCluster.addressId).enqueue(object :
                Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val address: Address? = parseApiResponseData(apiResponse.data)
                            address?.let {
                                binding.tvLocation.text =
                                    "${it.street}, ${it.ward}, ${it.district}, ${it.city}"
                            }
                        }
                    } else {
                        binding.tvLocation.text = "Địa chỉ không khả dụng"
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    binding.tvLocation.text = "Lỗi lấy địa chỉ"
                }
            })

            // Set court name
            binding.tvCourtName.text = statistic.courtCluster.name

            // Set revenue
            binding.tvRevenue.text = "Doanh thu: ${statistic.price}₫"

            // Set booking count
            binding.tvBookings.text = "Số lượt đặt sân: ${statistic.bookingCount}"
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(statisticData)
        }
    }
}
