package com.maxholmes.androidapp.screen.customer.search.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.databinding.ItemCourtClusterBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl

class CourtClusterSearchAdapter : RecyclerView.Adapter<CourtClusterSearchAdapter.ViewHolder>() {

    private val courtClusters = mutableListOf<CourtCluster>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<CourtCluster>? = null

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val binding = ItemCourtClusterBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.bindViewData(courtClusters[position])
    }

    override fun getItemCount(): Int = courtClusters.size

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<CourtCluster>) {
        onItemClickListener = listener
    }

    fun updateData(newCourtClusters: MutableList<CourtCluster>?) {
        newCourtClusters?.let {
            courtClusters.clear()
            courtClusters.addAll(it)
            notifyDataSetChanged()
        }
    }

    class ViewHolder(
        private val binding: ItemCourtClusterBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<CourtCluster>?
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
            binding.addressCourtClusterTextView.text = courtCluster.address.toString()
            binding.ratingText.text = "0 (rate)"
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(courtClusterData)
        }
    }
}