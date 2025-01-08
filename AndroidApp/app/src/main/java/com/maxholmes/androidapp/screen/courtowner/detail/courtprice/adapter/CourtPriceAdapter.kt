package com.maxholmes.androidapp.screen.courtowner.detail.courtprice

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.data.model.CourtPrice
import com.maxholmes.androidapp.databinding.ItemCourtPriceBinding
import com.maxholmes.androidapp.utils.ext.formatTime

class CourtPriceAdapter : RecyclerView.Adapter<CourtPriceAdapter.CourtPriceViewHolder>() {
    private val courtPrices = mutableListOf<CourtPrice>()
    private var onDeleteClick: (CourtPrice) -> Unit = {}
    private var onItemClick: (CourtPrice) -> Unit = {}


    class CourtPriceViewHolder(
        private val binding: ItemCourtPriceBinding,
        private val onDeleteClick: (CourtPrice) -> Unit,
        private val onItemClick: (CourtPrice) -> Unit
    ) : RecyclerView.ViewHolder(binding.root) {

        fun bind(courtPrice: CourtPrice) {
            with(binding) {
                timeTextView.text = courtPrice.time
                tvPrice.setText(courtPrice.price.toString())

                btnDelete.setOnClickListener {
                    onDeleteClick(courtPrice)
                }

                root.setOnClickListener {
                    onItemClick(courtPrice)
                }
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CourtPriceViewHolder {
        val binding = ItemCourtPriceBinding.inflate(
            LayoutInflater.from(parent.context), parent, false
        )
        return CourtPriceViewHolder(binding, onDeleteClick, onItemClick)
    }

    override fun onBindViewHolder(holder: CourtPriceViewHolder, position: Int) {
        holder.bind(courtPrices[position])
    }

    override fun getItemCount(): Int = courtPrices.size

    fun updateData(newCourtPrices: MutableList<CourtPrice>) {
        courtPrices.clear()
        courtPrices.addAll(newCourtPrices)
        notifyDataSetChanged()
    }

    fun addCourtPrice(courtPrice: CourtPrice) {
        courtPrices.add(courtPrice)
        notifyItemInserted(courtPrices.size - 1)
    }

    fun removeCourtPrice(courtPrice: CourtPrice) {
        val index = courtPrices.indexOf(courtPrice)
        if (index != -1) {
            courtPrices.removeAt(index)
            notifyItemRemoved(index)
        }
    }

    fun contains(courtPrice: CourtPrice): Boolean {
        return courtPrices.any { it.time.formatTime() == courtPrice.time.formatTime() }
    }

    fun setOnDeleteClickListener(listener: (CourtPrice) -> Unit) {
        onDeleteClick = listener
    }

    fun getData(): List<CourtPrice> = courtPrices
}
