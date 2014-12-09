//
//  FriendRequestViewController.swift
//  Frontend2
//
//  Created by Ryan Tabler on 12/8/14.
//  Copyright (c) 2014 Ryan Tabler. All rights reserved.
//

import Foundation
import UIKit

class FriendRequestViewController: UIViewController {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Call GetFriendRequests
        // If I get friend requests, display the first one in TextBox
        // Else display "No pending friend requests"
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    @IBOutlet weak var TextBox: UITextView!
    
    @IBAction func AttemptToAccept(sender: UIButton) {
        // If there is a friend request
            // Call UpdateFriendRequest(string friendRequestId, bool accept)
        // Else
            // Do nothing
    }
    @IBAction func AttemptToDeny(sender: AnyObject) {
        // If there is a friend request
            // Call UpdateFriendRequest(string friendRequestId, bool accept)
        // Else
            // Do nothing
    }
    
    
    @IBAction func dismiss(sender: UIButton) {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
}