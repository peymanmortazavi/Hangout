//
//  LoginViewController.swift
//  Hangout
//
//  Created by Vahid Mazdeh on 11/7/14.
//  Copyright (c) 2014 Peyman Mortazavi. All rights reserved.
//

import Foundation
import UIKit


class LoginViewController: UIViewController {
    @IBOutlet weak var firstNameLabel: UILabel!
    
    @IBOutlet weak var emailTextField: UITextField!
    @IBOutlet weak var passwordTextField: UITextField!
    // Currently trying to get dismiss the keyboard when user taps out. I know I need to call emailTextField.resignFirstResponder at the right time
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        var client: HangoutClient = HangoutClient(apiUrl: "http://10.202.21.182:8080/")
        firstNameLabel.text = client.testGet()
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
}
