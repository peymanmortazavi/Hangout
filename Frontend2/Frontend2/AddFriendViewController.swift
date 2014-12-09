//
//  AddFriendViewController.swift
//  Frontend2
//
//  Created by Ryan Tabler on 12/8/14.
//  Copyright (c) 2014 Ryan Tabler. All rights reserved.
//

import Foundation
import UIKit

class AddFriendViewController: UIViewController {
    

    
    override func viewDidLoad() {
        super.viewDidLoad()
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBOutlet weak var TextBox: UITextView!
    
    @IBAction func AttemptToAddFriend(sender: UIButton) {
        // Take text from TextBox
        // Check that it is nonempty and chars are all digits
        // Call LookupUsers(string[] phones) to get id
        // Call CreateFriendRequest(string id)
    }
    
    @IBAction func dismiss(sender: UIButton) {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
}