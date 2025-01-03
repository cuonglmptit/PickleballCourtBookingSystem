import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.data.model.CourtPrice
import java.time.format.DateTimeFormatter

class PriceAdapter(private val prices: List<CourtPrice>) : RecyclerView.Adapter<PriceAdapter.PriceViewHolder>() {

    // Tạo ViewHolder cho từng item trong danh sách
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PriceViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(android.R.layout.simple_list_item_2, parent, false)
        return PriceViewHolder(view)
    }

    // Liên kết dữ liệu với từng View trong ViewHolder
    override fun onBindViewHolder(holder: PriceViewHolder, position: Int) {
        val courtPrice = prices[position]
        val timeFormatter = DateTimeFormatter.ofPattern("HH:mm")

        holder.timeTextView.text = courtPrice.time.format(timeFormatter)
        holder.priceTextView.text = "${courtPrice.price} ₫"
    }

    override fun getItemCount(): Int {
        return prices.size
    }

    class PriceViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val timeTextView: TextView = view.findViewById(android.R.id.text1)
        val priceTextView: TextView = view.findViewById(android.R.id.text2)
    }
}
