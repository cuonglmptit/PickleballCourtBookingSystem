package com.maxholmes.androidapp.utils.enum

enum class PaymentStatusEnum(val value: String) {
    Unpaid("Unpaid"),
    Paid("Paid");

    companion object {
        fun fromValue(value: String): PaymentStatusEnum {
            return values().firstOrNull { it.value.equals(value, ignoreCase = true) }
                ?: Unpaid
        }
    }
}

enum class BookingStatusEnum(val value: String) {
    Pending("Pending"),
    CourtOwnerConfirmed("CourtOwnerConfirmed"),
    Canceled("Canceled"),
    All("All");

    companion object {
        fun fromValue(value: String): BookingStatusEnum {
            return values().firstOrNull { it.value.equals(value, ignoreCase = true) }
                ?: Pending
        }
    }
}
