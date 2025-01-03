package com.maxholmes.androidapp.screen

import android.content.Intent
import android.os.Bundle
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.screen.authentication.LoginActivity
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.admin.HomeAdminActivity
import com.maxholmes.androidapp.utils.base.BaseActivity
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.authentication.decodeJWT

class MainActivity : BaseActivity() {

    override fun getLayoutResourceId(): Int {
        return R.layout.activity_main
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val token = SharedPreferencesUtils.getToken(this)

        if (token == null) {
            val loginIntent = Intent(this, LoginActivity::class.java)
            startActivity(loginIntent)
            finish()
            return
        }

        val claims = decodeJWT(token)
        val role = claims["role"] as? String ?: "No role"

        val nextActivity = when (role) {
            "Customer" -> HomeCustomerActivity::class.java
            "CourtOwner" -> HomeCourtOwnerActivity::class.java
            "Admin" -> HomeAdminActivity::class.java
            else -> {
                val loginIntent = Intent(this, LoginActivity::class.java)
                startActivity(loginIntent)
                finish()
                return
            }
        }

        val intent = Intent(this, nextActivity)
        intent.putExtra("TOKEN", token)
        startActivity(intent)
        finish()
    }

    override fun initView() {
    }
}
