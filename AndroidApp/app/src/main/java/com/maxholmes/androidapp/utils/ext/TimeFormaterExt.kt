package com.maxholmes.androidapp.utils.ext

import java.time.LocalDateTime
import java.time.LocalTime
import java.time.format.DateTimeFormatter

fun String.formatTime(): LocalTime {
    val formatter = DateTimeFormatter.ofPattern("HH:mm:ss")
    val time = LocalTime.parse(this, formatter)
    return time
}

fun LocalTime.formatTimeToString(): String {
    val formatter = DateTimeFormatter.ofPattern("HH:mm")
    return this.format(formatter)
}

fun String.formatTimeToStringWithSecond(): String {
    val formatter = DateTimeFormatter.ofPattern("HH:mm:ss")
    val time = LocalTime.parse(this + ":00", formatter)
    return time.format(formatter)
}

fun String.parseToLocalDateTime(): LocalDateTime {
    return LocalDateTime.parse(this, DateTimeFormatter.ISO_LOCAL_DATE_TIME)
}