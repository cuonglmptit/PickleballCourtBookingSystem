package com.maxholmes.androidapp.utils.ext
import android.widget.ImageView
import com.bumptech.glide.Glide
import com.bumptech.glide.request.RequestOptions
import java.net.URL
import java.util.Base64

fun ImageView.loadImageCircleWithUrl(
    url: String,
    placeholderResId: Int,
) {
    Glide.with(this.context)
        .load(url)
        .apply(RequestOptions().circleCrop().placeholder(placeholderResId).error(placeholderResId))
        .into(this)
}

fun ImageView.loadImageWithUrl(url: String, placeholderResId: Int) {
    Glide.with(this.context)
        .load(url)
        .apply(RequestOptions().placeholder(placeholderResId).error(placeholderResId))
        .into(this)
}


fun getImageAsBase64FromUrl(urlString: String): String? {
    return try {
        val url = URL(urlString)
        val bytes = url.readBytes()
        Base64.getEncoder().encodeToString(bytes)
    } catch (e: Exception) {
        e.printStackTrace()
        null
    }
}

data class ImageObject(
    val fileName: String,
    val fileContent: String
)

fun createImageObjectFromUrl(urlString: String): ImageObject? {
    val base64Content = getImageAsBase64FromUrl(urlString)
    return base64Content?.let {
        ImageObject(
            fileName = urlString.substringAfterLast('/'),
            fileContent = it
        )
    }
}
