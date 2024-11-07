package com.maxholmes.androidapp.screen.home.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.content.res.AppCompatResources
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.databinding.ItemCourtBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl
import com.maxholmes.androidapp.utils.ext.loadImageWithUrl

class CourtAdapter : RecyclerView.Adapter<CourtAdapter.ViewHolder>() {
    private val courts = mutableListOf<Court>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<Court>? = null

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int,
    ): ViewHolder {
        val binding = ItemCourtBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onItemClickListener)
    }

    override fun onBindViewHolder(
        holder: ViewHolder,
        position: Int,
    ) {
        holder.bindViewData(courts[position])
    }

    override fun getItemCount(): Int {
        return courts.size
    }

    fun registerItemRecyclerViewClickListener(listener: OnItemRecyclerViewClickListener<Court>) {
        onItemClickListener = listener
    }

    fun updateData(courts: MutableList<Court>?) {
        courts?.let {
            this.courts.clear()
            this.courts.addAll(it)
            notifyDataSetChanged()
        }
    }

    class ViewHolder(
        private val binding: ItemCourtBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<Court>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var courtData: Court? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(court: Court) {
            courtData = court

            binding.courtImage.loadImageCircleWithUrl(
                court.imageUrl, R.drawable.image_court_1
            )

//            binding.courtImage.setImageResource(R.drawable.image_court_1)

            binding.courtNameTextView.text = court.name

            binding.locationCourt.text = court.addressId

            binding.courtSizeTextView.text = "${court.length} m × ${court.width} m"

            binding.textViewPrice.text = "₫150.000"
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(courtData)
        }
    }
}
