package com.maxholmes.androidapp.screen.test

import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R

class TestActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_test)

        val token = intent.getStringExtra("TOKEN")
        val tokenTextView: TextView = findViewById(R.id.tokenTextView)
        tokenTextView.text = "Token: $token"
    }
}
