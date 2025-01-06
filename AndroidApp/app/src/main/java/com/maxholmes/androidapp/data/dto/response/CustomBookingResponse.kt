package com.pickleballcourtbookingsystem.api.dtos

import android.os.Parcelable
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.data.model.Booking
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import kotlinx.parcelize.Parcelize

@Parcelize
data class CustomBookingResponse(
    val booking: Booking? = null,
    val courtClusterName: String? = null,
    val courtNumber: Int? = null,
    val address: Address? = null,
    val courtTimeSlots: List<CourtTimeSlot>? = null,
    val courtOwnerPhoneNumber: String? = null,
    val customerPhoneNumber: String? = null,
    val lastUpdatedTime: String
) : Parcelable
