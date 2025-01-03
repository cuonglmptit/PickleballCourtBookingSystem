package com.maxholmes.androidapp.screen.customer.home.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ItemCourtClusterBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import kotlin.random.Random

class CourtClusterAdapter : RecyclerView.Adapter<CourtClusterAdapter.ViewHolder>() {
    private val courtClusters = mutableListOf<CourtCluster>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<CourtCluster>? = null

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int,
    ): ViewHolder {
        val binding = ItemCourtClusterBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(
        holder: ViewHolder,
        position: Int,
    ) {
        holder.bindViewData(courtClusters[position])
    }

    override fun getItemCount(): Int {
        return courtClusters.size
    }

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<CourtCluster>) {
        onItemClickListener = listener
    }

    fun updateData(courtClusters: MutableList<CourtCluster>?) {
        courtClusters?.let {
            this.courtClusters.clear()
            this.courtClusters.addAll(it)
            notifyDataSetChanged()
        }
    }

    class ViewHolder(
        private val binding: ItemCourtClusterBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<CourtCluster>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var courtClusterData: CourtCluster? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(courtCluster: CourtCluster) {
            courtClusterData = courtCluster

            courtCluster.imageUrl?.let {
                binding.courtClusterImage.loadImageCircleWithUrl(it, R.drawable.image_court_1)
            }

            binding.courtClusterNameTextView.text = courtCluster.name

            // Chỉ hiển thị thông tin địa chỉ khi đã lấy được từ API
            binding.addressCourtClusterTextView.text = "Loading address..."

            // Gọi API để lấy địa chỉ từ addressId
            RetrofitClient.ApiClient.apiService.getAddressById(courtCluster.addressId).enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val address: Address? = parseApiResponseData(apiResponse.data)
                            address?.let {
                                binding.addressCourtClusterTextView.text =
                                    "${it.street}, ${it.ward}, ${it.district}, ${it.city}"
                            }
                        }
                    } else {
                        binding.addressCourtClusterTextView.text = "Address not available"
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    binding.addressCourtClusterTextView.text = "Error fetching address"
                }
            })

            binding.ratingText.text = "0 (rate)"
            var x = Random.nextInt(2)
            binding.bookingsText.text = if (x == 0) "0 lượt đặt" else "1 lượt đặt"
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(courtClusterData)
        }
    }

}
