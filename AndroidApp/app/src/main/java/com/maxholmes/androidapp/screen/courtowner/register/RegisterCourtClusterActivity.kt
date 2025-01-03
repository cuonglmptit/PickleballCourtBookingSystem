package com.maxholmes.androidapp.screen.courtowner.register

import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.Spinner
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R

class RegisterCourtClusterActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register_court_cluster)

        val openTimeSpinner: Spinner = findViewById(R.id.openTimeSpinner)
        val closeTimeSpinner: Spinner = findViewById(R.id.closeTimeSpinner)

        val timeList = generateTimeList()

        val timeAdapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, timeList)
        timeAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)

        openTimeSpinner.adapter = timeAdapter
        closeTimeSpinner.adapter = timeAdapter
    }

    // Hàm để tạo danh sách thời gian từ 05:00 đến 22:00
    private fun generateTimeList(): List<String> {
        val timeList = mutableListOf<String>()
        for (hour in 5..22) {
            for (minute in listOf("00", "30")) {
                timeList.add(String.format("%02d:%s", hour, minute))
            }
        }
        return timeList
    }
}
