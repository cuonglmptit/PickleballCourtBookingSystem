package com.maxholmes.androidapp.screen.home.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.databinding.ItemSelectCourtBinding
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener

class SelectCourtAdapter : RecyclerView.Adapter<SelectCourtAdapter.ViewHolder>() {
    private val courts = mutableListOf<Court>()
    private var onItemClickListener: OnItemRecyclerViewClickListener<Court>? = null

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int,
    ): ViewHolder {
        val binding = ItemSelectCourtBinding.inflate(LayoutInflater.from(parent.context), parent, false)
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
        private val binding: ItemSelectCourtBinding,
        private val itemClickListener: OnItemRecyclerViewClickListener<Court>?,
    ) : RecyclerView.ViewHolder(binding.root), View.OnClickListener {
        private var courtData: Court? = null

        init {
            binding.root.setOnClickListener(this)
        }

        fun bindViewData(court: Court) {
            courtData = court

            val courtLabel = binding.root.context.getString(R.string.court_label)
            val courtName = "$courtLabel ${court.courtNumber}"
            binding.courtNameTextView.text = courtName
        }

        override fun onClick(view: View?) {
            itemClickListener?.onItemClick(courtData)
        }
    }
}
