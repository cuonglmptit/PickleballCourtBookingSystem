package com.maxholmes.androidapp.utils.ext

import java.util.Calendar

fun Calendar.getDayOfMonth(): Int {
    return this.get(Calendar.DAY_OF_MONTH)
}

fun Calendar.getDayOfWeekName(): String {
    val dayOfWeek = this.get(Calendar.DAY_OF_WEEK)

    return when (dayOfWeek) {
        Calendar.SUNDAY -> "Chủ Nhật"
        Calendar.MONDAY -> "Thứ Hai"
        Calendar.TUESDAY -> "Thứ Ba"
        Calendar.WEDNESDAY -> "Thứ Tư"
        Calendar.THURSDAY -> "Thứ Năm"
        Calendar.FRIDAY -> "Thứ Sáu"
        Calendar.SATURDAY -> "Thứ Bảy"
        else -> "Không xác định"
    }
}