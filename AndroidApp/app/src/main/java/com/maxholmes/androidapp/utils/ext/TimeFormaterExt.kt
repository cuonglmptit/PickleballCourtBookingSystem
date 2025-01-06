package com.maxholmes.androidapp.utils.ext

import java.time.LocalDateTime
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

fun formatTimeToStringWithSecond(timeString: String): String {
    val formatter = DateTimeFormatter.ofPattern("HH:mm:ss")
    val time = LocalTime.parse(timeString + ":00", formatter)
    return time.format(formatter)
}

fun parseToLocalDateTime(timeString: String): LocalDateTime {
    return LocalDateTime.parse(timeString, DateTimeFormatter.ISO_LOCAL_DATE_TIME)
}