package com.maxholmes.androidapp.utils.ext

fun generateTimeList(): List<String> {
    val timeList = List(24) { String.format("%02d:00", it) }
    return timeList
}