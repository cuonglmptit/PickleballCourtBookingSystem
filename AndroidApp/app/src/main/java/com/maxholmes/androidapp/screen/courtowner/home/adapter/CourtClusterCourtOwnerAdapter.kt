package com.maxholmes.androidapp.screen.customer.home.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.model.ImageCourtUrl
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ItemCourtClusterCourtownerBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.enum.CourtClusterStatus
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl
import com.maxholmes.androidapp.utils.ext.loadImageWithUrl
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CourtClusterCourtOwnerAdapter : RecyclerView.Adapter<CourtClusterCourtOwnerAdapter.ViewHolder>() {
    private val courtClusters = mutableListOf<CourtCluster>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<CourtCluster>? = null

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int,
    ): ViewHolder {
        val binding = ItemCourtClusterCourtownerBinding.inflate(LayoutInflater.from(parent.context), parent, false)
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
        private val binding: ItemCourtClusterCourtownerBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<CourtCluster>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var courtClusterData: CourtCluster? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(courtCluster: CourtCluster) {
            courtClusterData = courtCluster
            val imageUrl = courtCluster.imageUrl ?: ""
            binding.courtClusterImage.loadImageCircleWithUrl(imageUrl, R.drawable.image_court_1)
            binding.courtClusterNameTextView.text = courtCluster.name

            binding.addressCourtClusterTextView.text = courtCluster.address.toString()
            if(courtCluster.status == CourtClusterStatus.Active.value) {
                binding.status.text = "Trạng thái: Hoạt động"
            }
            else
            {
                binding.status.text = "Trạng thái: Ngưng hoạt động"
            }
            binding.ratingText.text = "0 (rate)"
            RetrofitClient.ApiClient.apiService.getImagesByClusterId(courtCluster.id).enqueue(object: Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val images: List<ImageCourtUrl>? = parseApiResponseData(apiResponse.data)
                            if (images != null && images.size != 0)
                            {
                                binding.courtClusterImage.loadImageWithUrl(images[0].url, R.drawable.image_court_1)
                            }
                            else
                            {
                                binding.courtClusterImage.setImageResource(R.drawable.image_court_1)
                            }
                        }
                    }
                }
                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                }
            })
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(courtClusterData)
        }
    }

}
