package com.maxholmes.androidapp.screen

import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.screen.home.HomeFragment
import com.maxholmes.androidapp.utils.base.BaseActivity

class MainActivity : BaseActivity() {
    override fun getLayoutResourceId(): Int {
        return R.layout.activity_main
    }

    override fun initView() {
        supportFragmentManager
            .beginTransaction()
            .addToBackStack(HomeFragment::javaClass.name)
            .replace(R.id.layoutContainer, HomeFragment.newInstance())
            .commit()
    }
}