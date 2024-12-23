package com.maxholmes.androidapp.screen

import android.content.Intent
import android.os.Bundle
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.screen.authentication.LoginActivity
import com.maxholmes.androidapp.utils.base.BaseActivity

class MainActivity : BaseActivity() {

    override fun getLayoutResourceId(): Int {
        return R.layout.activity_main
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // Chuyển sang LoginActivity
        val intent = Intent(this, LoginActivity::class.java)
        startActivity(intent)

        finish()
    }

    override fun initView() {
    }
}
