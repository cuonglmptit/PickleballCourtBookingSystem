package com.maxholmes.androidapp.utils.base

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView
import com.maxholmes.androidapp.R

abstract class BaseSpinnerAdapter<T>(
    context: Context,
    private val items: ArrayList<T>,
    private val layoutResId: Int
) : ArrayAdapter<T>(context, layoutResId, items) {

    abstract fun getItemText(item: T?): String

    override fun getView(
        position: Int,
        convertView: View?,
        parent: ViewGroup
    ): View {
        val view = convertView ?: LayoutInflater.from(context).inflate(layoutResId, parent, false)
        val item = getItem(position)
//        val textView = view.findViewById<TextView>(R.id.textviewSpinner)
//        textView.text = getItemText(item)
        return view
    }

    override fun getDropDownView(
        position: Int,
        convertView: View?,
        parent: ViewGroup
    ): View {
        return getView(position, convertView, parent)
    }

    fun updateData(newItems: ArrayList<T>) {
        items.clear()
        items.addAll(newItems)
        notifyDataSetChanged()
    }
}