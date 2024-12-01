package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class Address(
    var id: String,
    var city: String,
    var district: String,
    var ward: String,
    var street: String
) : Parcelable
