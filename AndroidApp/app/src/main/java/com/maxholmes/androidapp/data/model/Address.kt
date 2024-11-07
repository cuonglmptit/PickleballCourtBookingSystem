package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
class Address (
    var id: String,
    var city: String,
    var district: String,
    var street: String
): Parcelable