package com.maxholmes.androidapp.screen.customer.booking

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityBookingBinding
import com.maxholmes.androidapp.screen.customer.booking.adapter.CourtTimeSlotAdapter
import com.maxholmes.androidapp.screen.customer.booking.adapter.SelectCourtAdapter
import com.maxholmes.androidapp.screen.customer.booking.adapter.SelectDayAdapter
import com.maxholmes.androidapp.screen.customer.booking.confirmbooking.ConfirmBookingActivity
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.time.LocalTime
import java.time.format.DateTimeFormatter
import java.util.Calendar

class BookingActivity : AppCompatActivity() {
    private lateinit var binding: ActivityBookingBinding
    private lateinit var selectCourtAdapter: SelectCourtAdapter
    private lateinit var selectDayAdapter: SelectDayAdapter
    lateinit var courtTimeSlotAdapter: CourtTimeSlotAdapter

    private var selectedDay: Calendar? = null
    private var selectedCourt: Court? = null
    private val selectedTimeSlots = mutableListOf<CourtTimeSlot>()
    private var courtCluster: CourtCluster? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBookingBinding.inflate(layoutInflater)
        setContentView(binding.root)

        selectCourtAdapter = SelectCourtAdapter()
        selectDayAdapter = SelectDayAdapter()
        courtTimeSlotAdapter = CourtTimeSlotAdapter()

        val selectedDayPosition = intent.getIntExtra("selectedDayPosition", RecyclerView.NO_POSITION)
        val selectedCourtPosition = intent.getIntExtra("selectedCourtPosition", RecyclerView.NO_POSITION)
        courtCluster = intent.getParcelableExtra("courtCluster", CourtCluster::class.java)

        binding.recyclerViewCourt.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewCourt.adapter = selectCourtAdapter

        binding.recyclerViewDay.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewDay.adapter = selectDayAdapter

        binding.recyclerViewTimeSlot.layoutManager = GridLayoutManager(this, 3, GridLayoutManager.VERTICAL, false)
        binding.recyclerViewTimeSlot.adapter = courtTimeSlotAdapter
        binding.confirmButton.setOnClickListener {
            onConfirmButtonClicked()
        }

        loadCourts()
        loadDays()
        updateSelectedDayAndCourt(selectedDayPosition, selectedCourtPosition)
        if (selectedCourt != null && selectedDay != null) {
            fetchCourtTimeSlots()
        }

        selectDayAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Calendar> {
            override fun onItemClick(item: Calendar?) {
                item?.let {
                    val position = selectDayAdapter.getSelectDays().indexOf(it)
                    if (position != RecyclerView.NO_POSITION) {
                        selectDayAdapter.setSelectedItem(position)
                        selectedDay = it
                        updateSelectedDayAndCourt(position, RecyclerView.NO_POSITION)
                        fetchCourtTimeSlots()
                    }
                }
            }
        })

        selectCourtAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Court> {
            override fun onItemClick(item: Court?) {
                item?.let {
                    val position = selectCourtAdapter.getCourts().indexOf(it)
                    if (position != RecyclerView.NO_POSITION) {
                        selectCourtAdapter.setSelectedItem(position)
                        selectedCourt = it
                        updateSelectedDayAndCourt(RecyclerView.NO_POSITION, position)
                        fetchCourtTimeSlots()
                    }
                }
            }
        })

        courtTimeSlotAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtTimeSlot> {
            override fun onItemClick(item: CourtTimeSlot?) {
                item?.let {
                    println("Da chon court time slot")
                    println("So phan tu la ${selectedTimeSlots.size}")
                    val position = courtTimeSlotAdapter.getCourtTimeSlots().indexOf(it)
                    if (position != RecyclerView.NO_POSITION) {
                        courtTimeSlotAdapter.setSelectedItem(position)

                        if (selectedTimeSlots.contains(it)) {
                            selectedTimeSlots.remove(it)
                        } else {
                            selectedTimeSlots.add(it)
                        }
                        courtTimeSlotAdapter.notifyItemChanged(position)
                    }
                }
            }
        })

    }

    private fun updateSelectedDayAndCourt(selectedDayPosition: Int, selectedCourtPosition: Int) {
        if (selectedDayPosition != RecyclerView.NO_POSITION) {
            selectDayAdapter.setSelectedItem(selectedDayPosition)
            selectedDay = selectDayAdapter.getSelectDays()[selectedDayPosition]
        }

        if (selectedCourtPosition != RecyclerView.NO_POSITION) {
            selectCourtAdapter.setSelectedItem(selectedCourtPosition)
            selectedCourt = selectCourtAdapter.getCourts()[selectedCourtPosition]
        }

    }



    private fun fetchCourtTimeSlots() {
        println("Da goi")
        val courtId = selectedCourt?.id ?: return
        println("courtId day")
        val date = selectedDay?.let {
            val formatter = java.text.SimpleDateFormat("yyyy-MM-dd", java.util.Locale.getDefault())
            formatter.format(it.time)
        } ?: return

        RetrofitClient.ApiClient.apiService.getCourtTimeSlotsForBooking(courtId, date).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val courtTimeSlots: List<CourtTimeSlot>? = parseApiResponseData(apiResponse.data)
                        courtTimeSlots?.let {
                            courtTimeSlotAdapter.updateData(it.toMutableList())
                        } ?: run {
                            Toast.makeText(this@BookingActivity, "Không có thời gian sân cho ngày đã chọn", Toast.LENGTH_SHORT).show()
                        }
                    }
                } else {
                    Toast.makeText(this@BookingActivity, "Lỗi khi lấy thời gian sân, vui lòng thử lại sau", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@BookingActivity, "Lỗi kết nối: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }



    private fun loadDays() {
        val calendar = Calendar.getInstance()
        val selectDays = mutableListOf<Calendar>()

        for (i in 0..6) {
            val day = calendar.clone() as Calendar
            day.add(Calendar.DAY_OF_YEAR, i)
            selectDays.add(day)
        }

        selectDayAdapter.updateData(selectDays)

        val selectedDayPosition = intent.getIntExtra("selectedDayPosition", RecyclerView.NO_POSITION)
        if (selectedDayPosition != RecyclerView.NO_POSITION) {
            selectDayAdapter.setSelectedItem(selectedDayPosition)
        }
    }

    private fun loadCourts() {
        val courts = intent.getParcelableArrayListExtra("courts", Court::class.java)
        selectCourtAdapter.updateData(courts)

        val selectedCourtPosition = intent.getIntExtra("selectedCourtPosition", RecyclerView.NO_POSITION)
        if (selectedCourtPosition != RecyclerView.NO_POSITION) {
            selectCourtAdapter.setSelectedItem(selectedCourtPosition)
        }
    }

    private fun onConfirmButtonClicked() {
        if (selectedCourt != null && selectedTimeSlots.isNotEmpty()) {
            println("Da click vao")
            val intent = Intent(this@BookingActivity, ConfirmBookingActivity::class.java)
            intent.putExtra("selectedCourt", selectedCourt)
            intent.putParcelableArrayListExtra("selectedTimeSlots", ArrayList(selectedTimeSlots))
            intent.putExtra("courtCluster", courtCluster)
            startActivity(intent)
        } else {
            Toast.makeText(this, "Vui lòng chọn sân và thời gian", Toast.LENGTH_SHORT).show()
        }
    }

}
