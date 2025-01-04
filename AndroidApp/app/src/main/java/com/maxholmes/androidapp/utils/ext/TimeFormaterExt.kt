package com.maxholmes.androidapp.utils.ext

import java.time.LocalTime
import java.time.format.DateTimeFormatter

fun formatTime(timeString: String): LocalTime {
    val formatter = DateTimeFormatter.ofPattern("HH:mm:ss")
    val time = LocalTime.parse(timeString, formatter)
    return time
}

fun formatTimeToString(timeLocal: LocalTime): String {
    val formatter = DateTimeFormatter.ofPattern("HH:mm")
    return timeLocal.format(formatter)
}