//
//  HomepageViewController.swift
//  Hangout
//
//  Created by Vahid Mazdeh on 11/7/14.
//  Copyright (c) 2014 Peyman Mortazavi. All rights reserved.
//

import Foundation
import UIKit


class HomepageViewController: UIViewController {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    @IBAction func AttemptToSignUp(sender: UIButton) {
        // Gather all text fields (first, last, email, password, phone number)
        // Check that phone number is only numerical characters
        // Check that all fields are non-empty
        // Format into JSON
        // Call Login(LoginModel)
        // If successful, move to Friends
        // Otherwise, clear password
    }
    
    @IBAction func dismiss(sender: UIButton) {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
}