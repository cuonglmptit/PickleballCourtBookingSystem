package com.maxholmes.androidapp.data.dto.response

import com.maxholmes.androidapp.data.model.Booking
import com.maxholmes.androidapp.data.model.CourtCluster


data class StatisticDto(
    var courtCluster: CourtCluster? = null,
    var bookings: List<Booking>? = null,
    var totalRevenue: Double = 0.0,
    var totalBookings: Int = 0
)
