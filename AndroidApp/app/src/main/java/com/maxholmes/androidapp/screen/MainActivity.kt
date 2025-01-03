package com.maxholmes.androidapp.screen

import android.content.Intent
import android.os.Bundle
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.screen.address.UpdateAddressActivity
import com.maxholmes.androidapp.screen.authentication.LoginActivity
import com.maxholmes.androidapp.screen.courtowner.add.AddCourtTimeSlotActivity
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.courtowner.register.RegisterCourtClusterActivity
import com.maxholmes.androidapp.screen.courtowner.statistics.StatisticActivity
import com.maxholmes.androidapp.screen.courtowner.update.UpdateCourtClusterActivity
import com.maxholmes.androidapp.screen.customer.booking.BookingActivity
import com.maxholmes.androidapp.screen.customer.booking.confirmbooking.ConfirmBookingActivity
import com.maxholmes.androidapp.screen.customer.detail.CourtClusterDetailActivity
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.screen.user.UpdateUserActivity
import com.maxholmes.androidapp.utils.base.BaseActivity

class MainActivity : BaseActivity() {

    override fun getLayoutResourceId(): Int {
        return R.layout.activity_main
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // Chuyển sang LoginActivity
        val intent = Intent(this, StatisticActivity::class.java)
        startActivity(intent)

        finish()
    }

    override fun initView() {
    }
}
