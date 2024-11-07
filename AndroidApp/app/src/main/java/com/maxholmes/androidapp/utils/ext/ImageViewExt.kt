package com.maxholmes.androidapp.utils.ext
import android.widget.ImageView
import com.bumptech.glide.Glide
import com.bumptech.glide.request.RequestOptions

fun ImageView.loadImageCircleWithUrl(
    url: String,
    placeholderResId: Int,
) {
    Glide.with(this.context)
        .load(url)
        .apply(RequestOptions().circleCrop().placeholder(placeholderResId).error(placeholderResId))
        .into(this)
}

fun ImageView.loadImageWithUrl(url: String) {
    Glide.with(this)
        .load(url)
        .into(this)
}