package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
class Court(
    var id: String,
    var name: String,
    var description: String,
    var length: Float,
    var width: Float,
    var addressId: String,
    var courtOwnerId: String,
    var imageUrl: String
) : Parcelable