package com.maxholmes.androidapp.screen.home.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.databinding.ItemCourtClusterBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl

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
                binding.courtClusterImage.loadImageCircleWithUrl(
                    it, R.drawable.image_court_1
                )
            }

//            binding.courtClusterImage.setImageResource(R.drawable.image_courtCluster_1)

            binding.courtClusterNameTextView.text = courtCluster.name

//            binding.addressCourtClusterTextView.text = courtCluster.addressId

        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(courtClusterData)
        }
    }
}
