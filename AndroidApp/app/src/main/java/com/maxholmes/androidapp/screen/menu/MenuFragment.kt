package com.maxholmes.androidapp.screen.menu

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.databinding.FragmentMenuBinding
import com.maxholmes.androidapp.utils.base.BaseFragment

class MenuFragment : BaseFragment<FragmentMenuBinding>(),
MenuContract.View{

    override fun inflateViewBinding(inflater: LayoutInflater): FragmentMenuBinding {
        TODO("Not yet implemented")
    }

    override fun initView() {
        TODO("Not yet implemented")
    }

    override fun initData() {
//        TODO("Not yet implemented")
    }

    override fun onError(exception: Exception?) {
        TODO("Not yet implemented")
    }

    companion object {
        fun newInstance() = MenuFragment()
    }
}