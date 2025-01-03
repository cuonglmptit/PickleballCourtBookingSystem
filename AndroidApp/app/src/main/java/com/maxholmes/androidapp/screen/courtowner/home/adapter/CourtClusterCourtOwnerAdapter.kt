package com.maxholmes.androidapp.screen.customer.home.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl
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
        val view = LayoutInflater.from(parent.context).inflate(R.layout.item_court_cluster_courtowner, parent, false)
        return ViewHolder(view, onItemClickListener)
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
        view: View,
        private val itemClickListener: OnItemRecyclerViewClickListener<CourtCluster>?,
    ) : RecyclerView.ViewHolder(view), View.OnClickListener {
        private var courtClusterData: CourtCluster? = null

        // Initialize views
        private val courtClusterImage: ImageView = view.findViewById(R.id.courtClusterImage)
        private val courtClusterNameTextView: TextView = view.findViewById(R.id.courtClusterNameTextView)
        private val addressCourtClusterTextView: TextView = view.findViewById(R.id.addressCourtClusterTextView)

        init {
            view.setOnClickListener(this)
        }

        fun bindViewData(courtCluster: CourtCluster) {
            courtClusterData = courtCluster

            // Load court image
            courtCluster.imageUrl?.let {
                courtClusterImage.loadImageCircleWithUrl(
                    it, R.drawable.image_court_1
                )
            }

            // Fetch address using Retrofit
            RetrofitClient.ApiClient.apiService.getAddressById(courtCluster.addressId).enqueue(object :
                Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val address: Address? = parseApiResponseData(apiResponse.data)
                            address?.let {
                                addressCourtClusterTextView.text =
                                    "${it.street}, ${it.ward}, ${it.district}, ${it.city}"
                            }
                        }
                    } else {
                        addressCourtClusterTextView.text = "Address not available"
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    addressCourtClusterTextView.text = "Error fetching address"
                }
            })

            // Set court cluster name
            courtClusterNameTextView.text = courtCluster.name
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(courtClusterData)
        }
    }
}
